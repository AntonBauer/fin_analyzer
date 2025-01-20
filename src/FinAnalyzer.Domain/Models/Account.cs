namespace FinAnalyzer.Domain.Models;

public sealed class Account
{
    public required Guid Id { get; init; }
    public required AccountInfo Info { get; init; }
    public Transaction[] Transactions { get; private set; } = [];

    public void AddTransactions(params Transaction[] transactions) =>
        Transactions = [.. Transactions, .. transactions];
}