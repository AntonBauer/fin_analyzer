using System.Collections.ObjectModel;

namespace FinAnalyzer.Domain.Models;

public sealed class Account
{
    public required Guid Id { get; init; }
    public required AccountInfo Info { get; init; }
    public ReadOnlyCollection<Transaction> Transactions { get; private set; } = ReadOnlyCollection<Transaction>.Empty;

    public void AddTransactions(params Transaction[] transactions) =>
        Transactions = Transactions.Union(transactions).ToArray().AsReadOnly();
}