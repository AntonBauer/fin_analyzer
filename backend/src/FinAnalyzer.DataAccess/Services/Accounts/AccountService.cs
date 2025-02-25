using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.Services.Accounts;

internal sealed class AccountService(FinAnalyzerContext context) : IAccountService
{
    public async Task<Account[]> ReadAll(CancellationToken cancellationToken) =>
        await context.Accounts.AsNoTracking().ToArrayAsync(cancellationToken);

    public async Task<Account> ReadAccount(Guid accountId,
                                           CancellationToken cancellationToken) =>
        await context.Accounts
                     .Include(account => account.Transactions)
                     .AsNoTracking()
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