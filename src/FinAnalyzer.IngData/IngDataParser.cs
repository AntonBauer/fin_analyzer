using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class IngDataParser(TransactionsParser transactionsParser) : IIngDataParser
{
    public async Task<RawTransaction[]> ParseTransactions(Stream transactionsFileStream,
                                                       CancellationToken cancellationToken)
    {
        var text = await ReadText(transactionsFileStream, cancellationToken);
        var splitted = SplitData(text);

        var transactions = await transactionsParser.ParseTransactions(splitted.TransactionsRaw);
        // ToDo: Add Account info parser

        return transactions;
    }

    private static async Task<string> ReadText(Stream transactionsFileStream,
                                        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(transactionsFileStream);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    private static (string AccountInfoRaw, string TransactionsRaw) SplitData(string rawText)
    {
        var splitted = rawText.Split("\n\n");
        return (splitted[1], splitted[^1]);
    }
}
