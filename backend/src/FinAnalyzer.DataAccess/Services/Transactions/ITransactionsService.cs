using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Transactions;

public interface ITransactionsService
{
    Task<Transaction[]> GetTransactions(Guid accountId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken);
    Task<Transaction> AssignCategory(Guid acocuntId, Guid transactionId, uint categoryId, CancellationToken cancellationToken);
    Task<Transaction> RemoveCategory(Guid acocuntId, Guid transactionId, CancellationToken cancellationToken);
}