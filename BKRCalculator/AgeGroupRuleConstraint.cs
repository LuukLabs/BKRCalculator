namespace KDVManager.BKRCalculator;
public class AgeGroupRuleConstraint
{
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxChildren { get; }

    public AgeGroupRuleConstraint(int minAge, int maxAge, int maxChildren)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxChildren = maxChildren;
    }

    public bool MeetsConstraint(AgeGroupCounts childrenCountByAge)
    {
        int count = childrenCountByAge.GetTotalChildrenBetweenAges(MinAge, MaxAge);

        return count <= MaxChildren;
    }
}