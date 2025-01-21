using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.Services.Transactions;

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

    public async Task<Transaction> AssignCategory(Guid acocuntId, Guid transactionId, uint categoryId, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
                                   .Include(account => account.Transactions.Where(transaction => transaction.Id == transactionId))
                                   .FirstOrDefaultAsync(account => account.Id == acocuntId, cancellationToken);

        var category = await context.Categories.FindAsync([categoryId], cancellationToken);

        var transaction = account.Transactions.Single();
        transaction.Cathegory = category;

        await context.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    public async Task<Transaction> RemoveCategory(Guid acocuntId, Guid transactionId, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
                                   .Include(account => account.Transactions.Where(transaction => transaction.Id == transactionId))
                                   .FirstOrDefaultAsync(account => account.Id == acocuntId, cancellationToken);

        var transaction = account.Transactions.Single();
        transaction.Cathegory = null;

        await context.SaveChangesAsync(cancellationToken);
        return transaction;
    }
}