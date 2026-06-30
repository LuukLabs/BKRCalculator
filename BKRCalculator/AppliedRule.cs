using System.Collections.Generic;

/// <summary>
/// The legal norm (one row of the BKR table) that matched the supplied group composition.
/// This is the structured "reason" a result applies: the age range, the maximum group size,
/// any sub-caps and the minimum number of professionals for that group.
///
/// Kept language-agnostic on purpose: the calculation library reports the facts, consumers
/// (frontend / API layer) turn them into localized text. Populated whenever a rule matches,
/// including results that only require a single professional.
///
/// Ages follow the rule convention: MinAge inclusive, MaxAge exclusive (e.g. 0-3 means
/// children aged 0, 1 and 2).
/// </summary>
public class AppliedRule
{
    public int MinAge { get; }
    public int MaxAge { get; }
    public int MaxChildren { get; }
    public int MinProfessionals { get; }
    public IReadOnlyList<AppliedRuleConstraint> Constraints { get; }

    /// <summary>
    /// The per-age ratios (1 professional per N children) for the age categories this rule spans,
    /// e.g. a 0-3 rule reports ages 0 (1:3), 1 (1:5) and 2 (1:6).
    /// </summary>
    public IReadOnlyList<AppliedRatio> Ratios { get; }

    public AppliedRule(int minAge, int maxAge, int maxChildren, int minProfessionals, IReadOnlyList<AppliedRuleConstraint> constraints, IReadOnlyList<AppliedRatio> ratios)
    {
        MinAge = minAge;
        MaxAge = maxAge;
        MaxChildren = maxChildren;
        MinProfessionals = minProfessionals;
        Constraints = constraints;
        Ratios = ratios;
    }
}
