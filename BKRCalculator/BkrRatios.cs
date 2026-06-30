namespace KDVManager.BKRCalculator;

/// <summary>
/// The per-age beroepskracht-kindratio for dagopvang (1 professional per N children),
/// as it applies since 1 July 2024. Single source of truth for both the calculation and the
/// ratios reported on an <see cref="AppliedRule"/>.
/// </summary>
internal static class BkrRatios
{
    private static readonly IReadOnlyDictionary<int, int> MaxChildrenPerProfessional =
        new Dictionary<int, int>
        {
            { 0, 3 }, // 0-1 jaar -> 1:3
            { 1, 5 }, // 1-2 jaar -> 1:5
            { 2, 6 }, // 2-3 jaar -> 1:6
            { 3, 8 }, // 3-4 jaar -> 1:8
        };

    public static bool Has(int age) => MaxChildrenPerProfessional.ContainsKey(age);

    public static int For(int age) => MaxChildrenPerProfessional[age];
}
