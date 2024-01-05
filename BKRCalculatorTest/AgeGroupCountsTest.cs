using KDVManager.BKRCalculator;

namespace KDVManager.BKRCalculatorTest;

[TestClass]
public class ChildrenCountsTests
{
    [TestMethod]
    public void GetTotalCount_WithValidCounts_ReturnsCorrectTotalCount()
    {
        // Arrange
        var childrenCounts = new AgeGroupCounts();
        childrenCounts.Age0Count = 2;
        childrenCounts.Age1Count = 3;
        childrenCounts.Age2Count = 1;
        childrenCounts.Age3Count = 4;

        // Act
        int totalCount = childrenCounts.TotalCount;

        // Assert
        Assert.AreEqual(10, totalCount);
    }

    [TestMethod]
    public void GetTotalCount_WithZeroCounts_ReturnsZeroTotalCount()
    {
        // Arrange
        var childrenCounts = new AgeGroupCounts();

        // Act
        int totalCount = childrenCounts.TotalCount;

        // Assert
        Assert.AreEqual(0, totalCount);
    }

    [TestMethod]
    public void GetMinAndMaxAge_WithValidCounts_ReturnsCorrectMinAndMaxAge()
    {
        // Arrange
        var childrenCounts = new AgeGroupCounts();
        childrenCounts.Age1Count = 2;
        childrenCounts.Age2Count = 5;
        childrenCounts.Age3Count = 3;

        // Act
        var (minAge, maxAge) = childrenCounts.GetMinAndMaxAge();

        // Assert
        Assert.AreEqual(1, minAge);
        Assert.AreEqual(3, maxAge);
    }

    [TestMethod]
    public void GetMinAndMaxAge_WithZeroCounts_ReturnsDefaultAges()
    {
        // Arrange
        var childrenCounts = new AgeGroupCounts();

        // Act
        var (minAge, maxAge) = childrenCounts.GetMinAndMaxAge();

        // Assert
        Assert.AreEqual(0, minAge);
        Assert.AreEqual(0, maxAge);
    }

    [TestMethod]
    public void GetTotalChildrenBetweenAges_WithValidCounts_ReturnsCorrectTotalCount()
    {
        // Arrange
        var childrenCounts = new AgeGroupCounts();
        childrenCounts.Age1Count = 2;
        childrenCounts.Age2Count = 5;
        childrenCounts.Age3Count = 3;

        // Act
        int totalCount = childrenCounts.GetTotalChildrenBetweenAges(1, 3);

        // Assert
        Assert.AreEqual(7, totalCount);
    }

    [TestMethod]
    public void GetAges_WithNonZeroCounts_ReturnsAgesWithNonZeroCounts()
    {
        // Arrange
        var ageGroupCounts = new AgeGroupCounts();
        ageGroupCounts.Age1Count = 2;
        ageGroupCounts.Age2Count = 0;
        ageGroupCounts.Age3Count = 5;

        // Act
        var nonZeroAges = ageGroupCounts.GetAges;

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 3 }, nonZeroAges);
    }

    [TestMethod]
    public void GetAges_WithZeroCounts_ReturnsEmptyList()
    {
        // Arrange
        var ageGroupCounts = new AgeGroupCounts();

        // Act
        var nonZeroAges = ageGroupCounts.GetAges;

        // Assert
        CollectionAssert.AreEqual(new int[0], nonZeroAges);
    }
}
