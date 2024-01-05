public class GroupAnalysisResult
{
    public int TotalChildren { get; }
    public bool HasSolution { get; }
    public int Professionals { get; }

    public GroupAnalysisResult(int totalChildren, bool hasSolution, int professionals)
    {
        TotalChildren = totalChildren;
        HasSolution = hasSolution;
        Professionals = professionals;
    }
}