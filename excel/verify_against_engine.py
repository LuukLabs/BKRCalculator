#!/usr/bin/env python3
"""
Verifies that the Excel template's formula logic matches the C# engine exactly.

It re-implements, in Python, the *same* algorithm the worksheet formulas encode
(see generate_template.py) and compares it against ground-truth output produced by
the real engine for every child-count combination.

Usage:
  1) Produce ground truth from the engine (all combinations, 0..16 per age):

     dotnet run, with a tiny program that loops the four counts and writes
     "c0,c1,c2,c3,hasSolution,professionals" to truth.csv. Example program body:

         var a = new GroupAnalyzer();
         for (c0..c3 in 0..16) {
             var r = a.CalculateBKR(new AgeGroupCounts{Age0Count=c0,...});
             write($"{c0},{c1},{c2},{c3},{(r.HasSolution?1:0)},{r.Professionals}");
         }

  2) python3 verify_against_engine.py truth.csv

Exit code 0 and "FAITHFUL" means the template reproduces the engine.
"""
import csv
import math
import sys

from generate_template import RULES


def _match_index(mn, mx, tot, x0):
    for i, (a, b, _mp, mc, mc0) in enumerate(RULES):
        if a == mn and b == mx + 1 and mc >= tot and mc0 >= x0:
            return i + 1  # 1-based, like Excel MATCH
    return 0


def _base(x0, x1, x2, x3):
    tot = x0 + x1 + x2 + x3
    if tot == 0:
        return 0
    mn = 0 if x0 > 0 else 1 if x1 > 0 else 2 if x2 > 0 else 3 if x3 > 0 else -1
    mx = 3 if x3 > 0 else 2 if x2 > 0 else 1 if x1 > 0 else 0 if x0 > 0 else -1
    idx = _match_index(mn, mx, tot, x0)
    if idx == 0:
        return 0
    mp = RULES[idx - 1][2]
    calc = math.ceil(x0 / 3 + (x1 / 5 + x2 / 6 + x3 / 8) / 1.2) if x0 > 0 else 0
    return max(mp, calc)


def result(c0, c1, c2, c3):
    """Returns (hasSolution, professionals), mirroring the Rekentool!B9 formula."""
    if c0 + c1 + c2 + c3 == 0:
        return (1, 0)
    orig = _base(c0, c1, c2, c3)
    if orig == 0:
        return (0, -1)  # total > 0 but no matching rule -> "Ongeldige groepssamenstelling"
    reduced = [
        _base(c0 - 1, c1, c2, c3) if c0 > 0 else 0,
        _base(c0, c1 - 1, c2, c3) if c1 > 0 else 0,
        _base(c0, c1, c2 - 1, c3) if c2 > 0 else 0,
        _base(c0, c1, c2, c3 - 1) if c3 > 0 else 0,
    ]
    return (1, orig + (1 if max(reduced) > orig else 0))


def main(path):
    mism = 0
    total = 0
    with open(path) as f:
        reader = csv.reader(f)
        next(reader)
        for row in reader:
            c0, c1, c2, c3, hs, prof = map(int, row)
            total += 1
            ehs, eprof = result(c0, c1, c2, c3)
            if (ehs, eprof) != (hs, prof):
                mism += 1
                if mism <= 15:
                    print(f"  MISMATCH ({c0},{c1},{c2},{c3}): engine=({hs},{prof}) excel=({ehs},{eprof})")
    print(f"compared {total} combinations, mismatches: {mism}")
    print("FAITHFUL" if mism == 0 else "DIVERGENCE")
    return 0 if mism == 0 else 1


if __name__ == "__main__":
    sys.exit(main(sys.argv[1] if len(sys.argv) > 1 else "truth.csv"))
