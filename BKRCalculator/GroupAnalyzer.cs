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

        var appliedRule = ageRange.ToAppliedRule();
        var professionals = ageRange.GetProfessionals(childrenCounts);

        // The minimum for the group size, or the weighted per-age ratio when it requires more.
        var basis = professionals > ageRange.MinProfessionals
            ? ProfessionalsBasis.RatioCalculation
            : ProfessionalsBasis.GroupSizeMinimum;

        if (TryAllCombinationsWithOneChildLess(childrenCounts, professionals))
        {
            return new GroupAnalysisResult(childrenCounts.TotalCount, true, professionals + 1, appliedRule, ProfessionalsBasis.OneChildLessSafeguard);
        }

        return new GroupAnalysisResult(childrenCounts.TotalCount, true, professionals, appliedRule, basis);
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
