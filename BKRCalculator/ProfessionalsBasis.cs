/// <summary>
/// Explains which mechanism set the final number of professionals — the "reason" behind the
/// number, beyond the matched <see cref="AppliedRule"/>. Lets consumers tell the user not just
/// which rule applies but why the count landed where it did.
/// </summary>
public enum ProfessionalsBasis
{
    /// <summary>No rule applied (no children present, or no matching rule was found).</summary>
    None,

    /// <summary>
    /// The minimum number of professionals for the matched group size was decisive
    /// (the per-age ratio calculation did not require more).
    /// </summary>
    GroupSizeMinimum,

    /// <summary>
    /// The weighted per-age ratio calculation (driven by the youngest children) required
    /// more professionals than the group-size minimum.
    /// </summary>
    RatioCalculation,

    /// <summary>
    /// An extra professional was added because removing one child from the group would push the
    /// required ratio higher — the safeguard against groups sitting just over a ratio threshold.
    /// </summary>
    OneChildLessSafeguard,
}
