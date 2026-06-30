# BKRCalculator

BKRCalculator is een C#-bibliotheek die berekent hoeveel beroepskrachten er minimaal nodig zijn op basis van het aantal kinderen per leeftijdsgroep. De rekenregels zijn gebaseerd op de Nederlandse beroepskracht-kindratio (BKR) zoals beschreven door [1Ratio](https://www.1ratio.nl/bkr/#/rekenregels/3).

## Installatie

Voeg BKRCalculator toe aan je project als package vanaf GitHub Packages:

```bash
$ dotnet add package BKRCalculator --version 0.2.0
```

## Overzicht

De bibliotheek bevat klassen om het aantal kinderen per leeftijdsgroep bij te houden en om het vereiste aantal beroepskrachten te berekenen. De ratio's per leeftijd (dagopvang, vanaf 1 juli 2024) zijn: 0 jaar 1:3 · 1 jaar 1:5 · 2 jaar 1:6 · 3 jaar 1:8.

## Functies

- **AgeGroupCounts:** houdt het aantal kinderen per leeftijdsgroep bij (0 t/m 3 jaar).
- **GroupAnalyzer:** berekent het vereiste aantal beroepskrachten op basis van die aantallen.
- **GroupAnalysisResult:** de uitkomst, inclusief een onderbouwing — welke wettelijke regel van toepassing is (`AppliedRule`) en waarom dat aantal volgt (`Basis`).

## Aan de slag

1. Maak een `AgeGroupCounts` aan en vul het aantal kinderen per leeftijd in.
2. Geef dit aan `GroupAnalyzer.CalculateBKR` mee.
3. Lees de uitkomst uit het `GroupAnalysisResult`.

```csharp
// Voorbeeld
var ageGroupCounts = new AgeGroupCounts
{
    Age0Count = 0,
    Age1Count = 5,
    Age2Count = 8,
    Age3Count = 3,
};

var groupAnalyzer = new GroupAnalyzer();
GroupAnalysisResult result = groupAnalyzer.CalculateBKR(ageGroupCounts);

Console.WriteLine($"Aantal kinderen: {result.TotalChildren}");
Console.WriteLine($"Geldige samenstelling: {result.HasSolution}");
Console.WriteLine($"Benodigde beroepskrachten: {result.Professionals}");
```

### De onderbouwing uitlezen

`GroupAnalysisResult` legt ook uit *waarom* een bepaald aantal van toepassing is. Dat is taalonafhankelijk: de bibliotheek geeft de feiten, de consument (frontend) maakt er leesbare tekst van.

```csharp
if (result.AppliedRule is { } rule)
{
    // De toegepaste wettelijke norm.
    Console.WriteLine($"Leeftijdsband {rule.MinAge}-{rule.MaxAge}, " +
                      $"max {rule.MaxChildren} kinderen, " +
                      $"minimaal {rule.MinProfessionals} beroepskrachten");

    foreach (var ratio in rule.Ratios)
        Console.WriteLine($"  {ratio.Age} jaar -> 1:{ratio.MaxChildrenPerProfessional}");
}

// Waarom dit aantal? GroupSizeMinimum, RatioCalculation of OneChildLessSafeguard.
Console.WriteLine($"Basis: {result.Basis}");
```

## Bijdragen

Bijdragen zijn welkom! Heb je ideeën, suggesties of een bug gevonden? Maak een issue aan of dien een pull request in.
