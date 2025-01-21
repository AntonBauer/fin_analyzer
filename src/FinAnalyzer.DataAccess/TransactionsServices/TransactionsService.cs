using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.TransactionsServices;

internal sealed class TransactionsService(FinAnalyzerContext context) : ITransactionsService
{
    public async Task<Transaction[]> GetTransactions(Guid accountId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
    {
        var account = await context.Accounts.AsNoTracking()
                                            .Include(account => account.Transactions.Where(t => from == null || t.RawTransaction.ValueDate >= from)
                                                                                    .Where(t => to == null || t.RawTransaction.ValueDate <= to))
                                            .FirstOrDefaultAsync(account => account.Id == accountId, cancellationToken);

        return [.. account.Transactions];
    }
}