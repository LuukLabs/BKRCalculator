/// <summary>
/// The beroepskracht-kindratio for one age category within the matched rule's age band:
/// one professional per <see cref="MaxChildrenPerProfessional"/> children of this age
/// (e.g. Age 0 -> 3 means 1:3). Reported so consumers can show the ratios that underpin the result.
/// </summary>
public class AppliedRatio
{
    public int Age { get; }
    public int MaxChildrenPerProfessional { get; }

    public AppliedRatio(int age, int maxChildrenPerProfessional)
    {
        Age = age;
        MaxChildrenPerProfessional = maxChildrenPerProfessional;
    }
}
