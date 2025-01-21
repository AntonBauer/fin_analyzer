using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Accounts;

public interface IAccountService
{
    Task<Account> ReadAccount(Guid accountId, CancellationToken cancellationToken);
    Task<Guid> UploadTransactions(AccountInfo accountInfo, Transaction[] transactions, CancellationToken cancellationToken);
}