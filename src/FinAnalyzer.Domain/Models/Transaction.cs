namespace FinAnalyzer.Domain.Models;

public sealed class Transaction
{
    public required Guid Id { get; init; }
    public required RawTransaction RawTransaction { get; init; }
}