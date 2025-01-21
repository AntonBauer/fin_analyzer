using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Transactions;

public interface ITransactionsService
{
    Task<Transaction[]> GetTransactions(Guid accountId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken);
}