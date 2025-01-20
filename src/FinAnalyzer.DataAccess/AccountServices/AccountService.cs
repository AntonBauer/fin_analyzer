using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.AccountServices;

internal sealed class AccountService(FinAnalyzerContext context) : IAccountService
{
    public async Task<Guid> SaveTransactions(AccountInfo accountInfo,
                                             Transaction[] transactions,
                                             CancellationToken cancellationToken)
    {
        var account = await context.Accounts
                                   .Where(account => account.Info.Iban == accountInfo.Iban)
                                   .FirstOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            account = new Account
            {
                Id = Guid.NewGuid(),
                Info = accountInfo
            };

            await context.Accounts.AddAsync(account, cancellationToken);
        }

        account.AddTransactions(transactions);

        await context.SaveChangesAsync(cancellationToken);
        return account.Id;
    }
}