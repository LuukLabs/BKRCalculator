/// <summary>
/// A sub-cap that was part of the matched rule, e.g. "at most 8 children aged 0-1".
/// Ages follow the same convention as the rules: MinAge inclusive, MaxAge exclusive.
/// Exposed so consumers can explain (in any language) why a rule applies.
/// </summary>
public class AppliedRuleConstraint
{
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxChildren { get; }

    public AppliedRuleConstraint(int minAge, int maxAge, int maxChildren)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxChildren = maxChildren;
    }
}
