public class GroupAnalysisResult
{
    public int TotalChildren { get; }
    public bool HasSolution { get; }
    public int Professionals { get; }

    /// <summary>
    /// The legal norm that matched the group composition, or <c>null</c> when no rule applied
    /// (no children, or no matching rule). Always populated when a rule matches, including
    /// results that only require a single professional.
    /// </summary>
    public AppliedRule? AppliedRule { get; }

    /// <summary>Which mechanism determined <see cref="Professionals"/>.</summary>
    public ProfessionalsBasis Basis { get; }

    public GroupAnalysisResult(int totalChildren, bool hasSolution, int professionals)
        : this(totalChildren, hasSolution, professionals, null, ProfessionalsBasis.None)
    {
    }

    public GroupAnalysisResult(int totalChildren, bool hasSolution, int professionals, AppliedRule? appliedRule, ProfessionalsBasis basis)
    {
        TotalChildren = totalChildren;
        HasSolution = hasSolution;
        Professionals = professionals;
        AppliedRule = appliedRule;
        Basis = basis;
    }
}
