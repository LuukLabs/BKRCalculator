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
}
