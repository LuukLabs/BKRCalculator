namespace KDVManager.BKRCalculator;

public class AgeGroupRule
{
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxChildren { get; }
    public int MinProfessionals { get; }
    public List<AgeGroupRuleConstraint> Constraints { get; }
    public AgeGroupRule(int minAge, int maxAge, int minProfessionals, int maxChildren, List<AgeGroupRuleConstraint> constraints = null)
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

    private int CalculateBKRFromCounts(AgeGroupCounts childrenCountByAge)
    {
        double A = childrenCountByAge.Age0Count / 3.0;
        double B = childrenCountByAge.Age1Count / 5.0;
        double C = childrenCountByAge.Age2Count / 6.0;
        double D = childrenCountByAge.Age3Count / 8.0;

        return (int)Math.Ceiling(A + ((B + C + D) / 1.2));
    }
}