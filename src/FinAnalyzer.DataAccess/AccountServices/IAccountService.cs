using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.AccountServices;

public interface IAccountService
{
    Task SaveTransactions(AccountInfo accountInfo, Transaction[] transactions, CancellationToken cancellationToken);
}