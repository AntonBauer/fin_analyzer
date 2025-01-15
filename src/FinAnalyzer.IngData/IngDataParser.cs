using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class IngDataParser : IIngDataParser
{
    public Task<Transaction[]> ParseTransactions(Stream transactionsFileStream) => Task.FromResult(Array.Empty<Transaction>());
}
