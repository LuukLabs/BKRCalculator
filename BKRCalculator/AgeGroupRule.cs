namespace KDVManager.BKRCalculator;

internal class AgeGroupRule
{
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxChildren { get; }
    public int MinProfessionals { get; }
    public List<AgeGroupRuleConstraint> Constraints { get; }
    public AgeGroupRule(int minAge, int maxAge, int minProfessionals, int maxChildren, List<AgeGroupRuleConstraint>? constraints = null)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MinProfessionals = minProfessionals;
        MaxChildren = maxChildren;
        Constraints = constraints ?? new List<AgeGroupRuleConstraint>();
    }

    public bool MeetsConstraint(AgeGroupCounts childrenCountByAge)
    {
        int totalChildren = childrenCountByAge.TotalCount;
        var (minAge, maxAge) = childrenCountByAge.GetMinAndMaxAge();

        return minAge == MinAge && maxAge + 1 == MaxAge && totalChildren <= MaxChildren && Constraints.All(c => c.MeetsConstraint(childrenCountByAge));
    }

    public int GetProfessionals(AgeGroupCounts childrenCountByAge)
    {
        if (childrenCountByAge.Age0Count > 0)
        {
            var calculatedProfessionals = CalculateBKRFromCounts(childrenCountByAge);

            if (calculatedProfessionals > MinProfessionals)
            {
                return calculatedProfessionals;
            }
        }
        return MinProfessionals;
    }

    public AppliedRule ToAppliedRule()
    {
        var constraints = Constraints
            .Select(c => new AppliedRuleConstraint(c.MinAge, c.MaxAge, c.MaxChildren))
            .ToList();

        var ratios = Enumerable.Range(MinAge, MaxAge - MinAge)
            .Where(BkrRatios.Has)
            .Select(age => new AppliedRatio(age, BkrRatios.For(age)))
            .ToList();

        return new AppliedRule(MinAge, MaxAge, MaxChildren, MinProfessionals, constraints, ratios);
    }

    private int CalculateBKRFromCounts(AgeGroupCounts childrenCountByAge)
    {
        double A = childrenCountByAge.Age0Count / (double)BkrRatios.For(0);
        double B = childrenCountByAge.Age1Count / (double)BkrRatios.For(1);
        double C = childrenCountByAge.Age2Count / (double)BkrRatios.For(2);
        double D = childrenCountByAge.Age3Count / (double)BkrRatios.For(3);

        return (int)Math.Ceiling(A + ((B + C + D) / 1.2));
    }
}