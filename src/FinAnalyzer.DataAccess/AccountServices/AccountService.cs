using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.AccountServices;

internal sealed class AccountService(FinAnalyzerContext context) : IAccountService
{
    public async Task SaveTransactions(AccountInfo accountInfo,
                                       Transaction[] transactions,
                                       CancellationToken cancellationToken)
    {
        // Check if there is account

        // Use existing or create new

        // Add Transactions to this account

        await context.SaveChangesAsync(cancellationToken);
        throw new NotImplementedException();
    }
}