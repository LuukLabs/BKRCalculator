namespace KDVManager.BKRCalculator;
public class GroupAnalyzer
{
    private List<AgeGroupRule> ageGroupRules;

    public GroupAnalyzer()
    {
        var ageGroupRulesFactory = new AgeGroupRulesFactory();
        ageGroupRules = ageGroupRulesFactory.BuildAgeGroupRules();
    }

    public GroupAnalysisResult CalculateBKR(AgeGroupCounts childrenCounts)
    {
        // Validate counts
        if (childrenCounts.TotalCount <= 0)
        {
            return new GroupAnalysisResult(childrenCounts.TotalCount, true, 0);
        }

        // Find ageRange
        var ageRange = ageGroupRules.FirstOrDefault(ar => ar.MeetsConstraint(childrenCounts));
        if (ageRange == null)
        {
            return new GroupAnalysisResult(childrenCounts.TotalCount, false, -1);
        }

        if (TryAllCombinationsWithOneChildLess(childrenCounts, ageRange.GetProfessionals(childrenCounts)))
        {
            return new GroupAnalysisResult(childrenCounts.TotalCount, true, ageRange.GetProfessionals(childrenCounts) + 1);
        }

        return new GroupAnalysisResult(childrenCounts.TotalCount, true, ageRange.GetProfessionals(childrenCounts));
    }
    private bool TryAllCombinationsWithOneChildLess(AgeGroupCounts childrenCounts, int original)
    {
        var ages = childrenCounts.GetAges;

        foreach (var age in ages)
        {
            var clonedCounts = childrenCounts.Clone();
            var originalCount = clonedCounts.GetCountForAge(age);

            clonedCounts.SetCountForAge(age, originalCount - 1);

            // Now you can use the modified counts for further processing or testing
            var ageRangeTest = ageGroupRules.FirstOrDefault(ar => ar.MeetsConstraint(clonedCounts));
            var professionalTest = ageRangeTest == null ? 0 : ageRangeTest.GetProfessionals(clonedCounts);

            if (professionalTest > original)
            {
                return true;
            }
        }
        return false;
    }
}
