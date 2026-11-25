namespace lb1.Models;

public enum RequirementKind
{
    Functional,
    NonFunctional,
    BusinessRule
}

public sealed record Requirement(
    string Id,
    RequirementKind Kind,
    string Title,
    string Statement,
    string AcceptanceCriteria,
    IReadOnlyList<string> RelatedEntities);
