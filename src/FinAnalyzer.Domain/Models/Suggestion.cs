namespace FinAnalyzer.Domain.Models;

public sealed class Suggestion
{
    public required Guid Id { get; init; }
    public required Transaction Transaction { get; init; }
    public Category? SuggestedCategory { get; init; }
}