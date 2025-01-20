using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.AccountServices;

public interface IAccountService
{
    Task<Guid> SaveTransactions(AccountInfo accountInfo, Transaction[] transactions, CancellationToken cancellationToken);
}