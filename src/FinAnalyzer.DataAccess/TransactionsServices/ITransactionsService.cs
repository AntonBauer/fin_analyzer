using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.TransactionsServices;

public interface ITransactionsService
{
    Task<Transaction[]> GetTransactions(Guid accountId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken);
}