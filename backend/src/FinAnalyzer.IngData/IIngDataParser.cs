using FinAnalyzer.IngData.Models;

namespace FinAnalyzer.IngData;

public interface IIngDataParser
{
    Task<TransactionsData> ParseTransactions(Stream transactionsFileStream, CancellationToken cancellationToken);
}