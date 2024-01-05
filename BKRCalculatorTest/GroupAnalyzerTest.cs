using KDVManager.BKRCalculator;

namespace KDVManager.BKRCalculatorTest;

[TestClass]
public class GroupAnalyzerTest
{
    [TestMethod]
    public void TestScenario2()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        AgeGroupCounts childrenCountByAge = new AgeGroupCounts
        {
            Age0Count = 3,
            Age1Count = 5,
            Age2Count = 8
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCountByAge);

        // Assert
        double expectedBKR = 4;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestScenario3()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        AgeGroupCounts childrenCountByAge = new AgeGroupCounts
        {
            Age0Count = 4,
            Age1Count = 0,
            Age2Count = 0,
            Age3Count = 12
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCountByAge);

        // Assert
        double expectedBKR = 4;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestScenario4()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        var childrenCounts = new AgeGroupCounts
        {
            Age1Count = 4,
            Age2Count = 4,
            Age3Count = 7
            // Add more age categories as needed for your scenario
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCounts);

        // Assert
        double expectedBKR = 3;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestScenario5()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        var childrenCounts = new AgeGroupCounts
        {
            Age0Count = 2,
            Age2Count = 1,
            Age3Count = 2
            // Add more age categories as needed for your scenario
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCounts);

        // Assert
        double expectedBKR = 2;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestScenario6()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        var childrenCounts = new AgeGroupCounts
        {
            Age0Count = 2,
            Age1Count = 2,
            Age3Count = 1
            // Add more age categories as needed for your scenario
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCounts);

        // Assert
        double expectedBKR = 2;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestScenario7()
    {
        // Arrange
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        var childrenCounts = new AgeGroupCounts
        {
            Age1Count = 1,
            Age2Count = 11,
            Age3Count = 1
            // Add more age categories as needed for your scenario
        };

        // Act
        double actualBKR = groupAnalyzer.CalculateBKR(childrenCounts);

        // Assert
        double expectedBKR = 3;
        double tolerance = 0.0001; // Adjust based on acceptable error margin
        Assert.AreEqual(expectedBKR, actualBKR, tolerance);
    }

    [TestMethod]
    public void TestCalculateBKRFromCounts_AllCombinations()
    {
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        for (int count0 = 0; count0 <= 16; count0++)
        {
            for (int count1 = 0; count1 <= 16; count1++)
            {
                for (int count2 = 0; count2 <= 16; count2++)
                {
                    for (int count3 = 0; count3 <= 16; count3++)
                    {
                        var childrenCountByAge = new AgeGroupCounts
                        {
                            Age0Count = count0,
                            Age1Count = count1,
                            Age2Count = count2,
                            Age3Count = count3
                        };

                        if (childrenCountByAge.TotalCount > 16)
                        {
                            continue;
                        }

                        try
                        {
                            double actualBKR = groupAnalyzer.CalculateBKR(childrenCountByAge);
                            Assert.IsTrue(actualBKR <= 4, $"BKR {actualBKR} exceeds 4 for counts: {count0}, {count1}, {count2}, {count3}");
                        }
                        catch (Exception ex)
                        {
                            Assert.Fail($"Unexpected exception for counts: {count0}, {count1}, {count2}, {count3} - {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}