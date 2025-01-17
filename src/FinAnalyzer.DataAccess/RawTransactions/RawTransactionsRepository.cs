using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.RawTransactions;

internal sealed class RawTransactionsRepository(FinAnalyzerContext context) : IRawTransactionsRepository
{
    public async Task Save(RawTransaction[] tranactions, CancellationToken cancellationToken)
    {
        await context.RawTransactions.AddRangeAsync(tranactions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}