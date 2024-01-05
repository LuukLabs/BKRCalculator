# BKRCalculator

BKRCalculator is a C# library that calculates the number of professionals needed based on the counts of children in different age groups. The calculation rules are based on the guidelines provided by [1Ratio](https://www.1ratio.nl/bkr/#/rekenregels/3).

## Installation

To use BKRCalculator in your project, you can add it as a package from GitHub Packages using the following command:

```bash
$ dotnet add package BKRCalculator --version 0.2.0
```

## Overview

The BKRCalculator library includes classes for managing age group counts, and calculating the required number of professionals .

## Features

- **AgeGroupCounts:** Track counts of children in different age groups.
- **GroupAnalyzer:** Calculate the required number of professionals based on age group counts.

## Getting Started
To use the BKRCalculator library, follow these steps:
- Create instances of AgeGroupCounts, and GroupAnalyzer.
- Set counts for different age groups using AgeGroupCounts.
- Use GroupAnalyzer to calculate the required number of professionals based on age group counts and constraints.

```csharp
// Example Usage
var ageGroupCounts = new AgeGroupCounts();
ageGroupCounts.Age1Count = 5;
ageGroupCounts.Age2Count = 8;
ageGroupCounts.Age3Count = 3;

var groupAnalyzer = new GroupAnaylyzer();
int calculatedProfessionals = groupAnalyzer.CalculateBKR(ageGroupCounts);
Console.WriteLine($"Calculated number of professionals: {calculatedProfessionals}");
```

### Contributing
Contributions are welcome! If you have any ideas, suggestions, or bug reports, please create an issue or submit a pull request.
