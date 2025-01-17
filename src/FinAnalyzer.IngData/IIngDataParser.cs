using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

public interface IIngDataParser
{
    Task<RawTransaction[]> ParseTransactions(Stream transactionsFileStream, CancellationToken cancellationToken);
}