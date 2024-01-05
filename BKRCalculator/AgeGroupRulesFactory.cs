namespace KDVManager.BKRCalculator;

internal class AgeGroupRulesFactory
{
    public List<AgeGroupRule> BuildAgeGroupRules()
    {
        var ageGroupRules = new List<AgeGroupRule>
        {
            new AgeGroupRule(0, 1, 1, 3),
            new AgeGroupRule(0, 1, 2, 6),
            new AgeGroupRule(0, 1, 3, 9),
            new AgeGroupRule(0, 1, 4, 12),

            new AgeGroupRule(1, 2, 1, 5),
            new AgeGroupRule(1, 2, 2, 10),
            new AgeGroupRule(1, 2, 3, 15),
            new AgeGroupRule(1, 2, 4, 16),

            new AgeGroupRule(2, 3, 1, 8),
            new AgeGroupRule(2, 3, 2, 16),

            new AgeGroupRule(3, 4, 1, 8),
            new AgeGroupRule(3, 4, 2, 16),

            new AgeGroupRule(0, 2, 1, 4),
            new AgeGroupRule(0, 2, 2, 8),
            new AgeGroupRule(0, 2, 3, 14, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),
            new AgeGroupRule(0, 2, 4, 16, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),

            new AgeGroupRule(0, 3, 1, 5),
            new AgeGroupRule(0, 3, 2, 10),
            new AgeGroupRule(0, 3, 3, 13, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),
            new AgeGroupRule(0, 3, 3, 14, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 4) }),
            new AgeGroupRule(0, 3, 3, 15, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 3) }),
            new AgeGroupRule(0, 3, 4, 16, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),

            new AgeGroupRule(0, 4, 1, 5),
            new AgeGroupRule(0, 4, 2, 12),
            new AgeGroupRule(0, 4, 3, 14, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),
            new AgeGroupRule(0, 4, 3, 15, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 5) }),
            new AgeGroupRule(0, 4, 3, 16, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 3) }),
            new AgeGroupRule(0, 4, 4, 16, new List<AgeGroupRuleConstraint> { new AgeGroupRuleConstraint(0, 1, 8) }),

            new AgeGroupRule(1, 3, 1, 6),
            new AgeGroupRule(1, 3, 2, 11),
            new AgeGroupRule(1, 3, 3, 16),

            new AgeGroupRule(1, 4, 1, 7),
            new AgeGroupRule(1, 4, 2, 13),
            new AgeGroupRule(1, 4, 3, 16),

            new AgeGroupRule(2, 4, 1, 8),
            new AgeGroupRule(2, 4, 2, 16),
        };

        return ageGroupRules;
    }
}
