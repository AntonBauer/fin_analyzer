using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.AccountServices;

internal sealed class AccountService(FinAnalyzerContext context) : IAccountService
{
    public async Task<Account> ReadAccount(Guid accountId,
                                           CancellationToken cancellationToken) =>
        // ToDo: add transactions from-to dates filter
        await context.Accounts
                     .AsNoTracking()
                     .Include(account => account.Transactions)
                     .ThenInclude(transaciton => transaciton.RawTransaction)
                     .FirstOrDefaultAsync(account => account.Id == accountId, cancellationToken);

    public async Task<Guid> UploadTransactions(AccountInfo accountInfo,
                                               Transaction[] transactions,
                                               CancellationToken cancellationToken)
    {
        var account = await EnsureAccount(accountInfo, cancellationToken);

        await context.Transactions.AddRangeAsync(transactions, cancellationToken);
        account.Transactions.AddRange(transactions);

        await context.SaveChangesAsync(cancellationToken);

        return account.Id;
    }

    private async Task<Account> EnsureAccount(AccountInfo accountInfo, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
                                   .FirstOrDefaultAsync(account => account.Info.Iban == accountInfo.Iban, cancellationToken);

        if (account is null)
        {
            account = new Account
            {
                Id = Guid.NewGuid(),
                Info = accountInfo
            };

            await context.Accounts.AddAsync(account, cancellationToken);
        }

        return account;
    }
}