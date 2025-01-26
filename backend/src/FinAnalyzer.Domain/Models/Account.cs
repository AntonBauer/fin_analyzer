namespace FinAnalyzer.Domain.Models;

public sealed class Account
{
    public required Guid Id { get; init; }
    public required AccountInfo Info { get; init; }
    public List<Transaction> Transactions { get; init; } = [];
}