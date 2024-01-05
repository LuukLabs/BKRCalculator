namespace KDVManager.BKRCalculator;

public class AgeGroupCounts
{
    private readonly Dictionary<int, int> counts = new Dictionary<int, int>();

    public int TotalCount => counts.Values.Sum();

    public (int minAge, int maxAge) GetMinAndMaxAge()
    {
        var nonZeroAges = counts
        .Where(ageCategory => ageCategory.Value > 0)
        .Select(ageCategory => ageCategory.Key);

        if (nonZeroAges.Any())
        {
            var minAge = nonZeroAges.Min();
            var maxAge = nonZeroAges.Max();

            return (minAge, maxAge);
        }

        return (0, 0);
    }
    public int GetTotalChildrenBetweenAges(int startAge, int endAge)
    {
        return counts.Where(pair => pair.Key >= startAge && pair.Key < endAge).Sum(pair => pair.Value);
    }

    public List<int> GetAges
    {
        get { return counts.Where(ageCategory => ageCategory.Value > 0).Select(ageCategory => ageCategory.Key).ToList(); }
    }

    public int Age0Count
    {
        get { return counts.ContainsKey(0) ? counts[0] : 0; }
        set { counts[0] = value; }
    }

    public int Age1Count
    {
        get { return counts.ContainsKey(1) ? counts[1] : 0; }
        set { counts[1] = value; }
    }

    public int Age2Count
    {
        get { return counts.ContainsKey(2) ? counts[2] : 0; }
        set { counts[2] = value; }
    }

    public int Age3Count
    {
        get { return counts.ContainsKey(3) ? counts[3] : 0; }
        set { counts[3] = value; }
    }
    private bool IsValidAge(int age)
    {
        return age >= 0 && age <= 3;
    }

    public void SetCountForAge(int age, int count)
    {
        if (IsValidAge(age))
        {
            counts[age] = Math.Max(0, count);
        }
    }

    public int GetCountForAge(int age)
    {
        return IsValidAge(age) ? counts.GetValueOrDefault(age, 0) : 0;
    }

    public AgeGroupCounts Clone()
    {
        var clonedCounts = new AgeGroupCounts();

        foreach (var kvp in counts)
        {
            clonedCounts.SetCountForAge(kvp.Key, kvp.Value);
        }

        return clonedCounts;
    }

}
