using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

public interface IIngDataParser
{
    Task<Transaction[]> ParseTransactions(Stream transactionsFileStream, CancellationToken cancellationToken);
}