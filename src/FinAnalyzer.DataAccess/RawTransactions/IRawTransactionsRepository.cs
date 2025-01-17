using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.RawTransactions;

public interface IRawTransactionsRepository
{
    Task Save(RawTransaction[] tranactions, CancellationToken cancellationToken);
}