using KDVManager.BKRCalculator;

namespace KDVManager.BKRCalculatorTest;

[TestClass]
public class GroupAnalyzerTest
{
    [TestMethod]
    [DataRow(3, 5, 8, 0, true, 4)]
    [DataRow(4, 0, 0, 12, true, 4)]
    [DataRow(0, 4, 4, 7, true, 3)]
    [DataRow(2, 0, 1, 2, true, 2)]
    [DataRow(2, 2, 0, 1, true, 2)]
    [DataRow(0, 1, 11, 1, true, 3)]
    public void TestCalculation(int age0, int age1, int age2, int age3, bool expectedHasSolution, int expectedProfessionals)
    {
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        // Arrange
        var childrenCounts = new AgeGroupCounts
        {
            Age0Count = age0,
            Age1Count = age1,
            Age2Count = age2,
            Age3Count = age3
        };

        // Act
        var result = groupAnalyzer.CalculateBKR(childrenCounts);

        // Assert
        Assert.AreEqual(age0 + age1 + age2 + age3, result.TotalChildren);
        Assert.AreEqual(expectedHasSolution, result.HasSolution);
        Assert.AreEqual(expectedProfessionals, result.Professionals);
    }

    [TestMethod]
    public void AppliedRule_ExposesMatchedNormAndConstraints()
    {
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts
        {
            Age0Count = 3,
            Age1Count = 5,
            Age2Count = 8,
        });

        Assert.IsNotNull(result.AppliedRule);
        Assert.AreEqual(0, result.AppliedRule!.MinAge);
        Assert.AreEqual(3, result.AppliedRule.MaxAge);
        Assert.AreEqual(16, result.AppliedRule.MaxChildren);
        Assert.AreEqual(4, result.AppliedRule.MinProfessionals);

        // Sub-cap: at most 8 children aged 0-1.
        Assert.HasCount(1, result.AppliedRule.Constraints);
        var cap = result.AppliedRule.Constraints[0];
        Assert.AreEqual(0, cap.MinAge);
        Assert.AreEqual(1, cap.MaxAge);
        Assert.AreEqual(8, cap.MaxChildren);
    }

    [TestMethod]
    public void AppliedRule_ReportsRatiosForAgeBand()
    {
        // Ages 0, 1 and 2 present -> rule spans the 0-3 band.
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts
        {
            Age0Count = 3,
            Age1Count = 5,
            Age2Count = 8,
        });

        Assert.IsNotNull(result.AppliedRule);
        var ratios = result.AppliedRule!.Ratios
            .ToDictionary(r => r.Age, r => r.MaxChildrenPerProfessional);

        CollectionAssert.AreEquivalent(new[] { 0, 1, 2 }, ratios.Keys.ToArray());
        Assert.AreEqual(3, ratios[0]); // 0-1 jaar -> 1:3
        Assert.AreEqual(5, ratios[1]); // 1-2 jaar -> 1:5
        Assert.AreEqual(6, ratios[2]); // 2-3 jaar -> 1:6
    }

    [TestMethod]
    public void Basis_IsGroupSizeMinimum_WhenGroupSizeNormIsDecisive()
    {
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts
        {
            Age0Count = 3,
            Age1Count = 5,
            Age2Count = 8,
        });

        Assert.AreEqual(ProfessionalsBasis.GroupSizeMinimum, result.Basis);
    }

    [TestMethod]
    public void Basis_IsRatioCalculation_WhenYoungChildrenDriveTheCount()
    {
        // Two babies push the weighted ratio calculation above the group-size minimum.
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts
        {
            Age0Count = 2,
            Age2Count = 1,
            Age3Count = 2,
        });

        Assert.AreEqual(2, result.Professionals);
        Assert.AreEqual(ProfessionalsBasis.RatioCalculation, result.Basis);
    }

    [TestMethod]
    public void Basis_IsOneChildLessSafeguard_WhenRemovingAChildWouldRaiseTheRatio()
    {
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts
        {
            Age1Count = 1,
            Age2Count = 11,
            Age3Count = 1,
        });

        Assert.AreEqual(3, result.Professionals);
        Assert.AreEqual(ProfessionalsBasis.OneChildLessSafeguard, result.Basis);
    }

    [TestMethod]
    public void Basis_IsNoneAndNoRule_ForEmptyGroup()
    {
        var result = new GroupAnalyzer().CalculateBKR(new AgeGroupCounts());

        Assert.IsTrue(result.HasSolution);
        Assert.AreEqual(0, result.Professionals);
        Assert.IsNull(result.AppliedRule);
        Assert.AreEqual(ProfessionalsBasis.None, result.Basis);
    }
}
