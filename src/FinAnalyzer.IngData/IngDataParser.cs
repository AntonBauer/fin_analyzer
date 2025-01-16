using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class IngDataParser : IIngDataParser
{
    public async Task<Transaction[]> ParseTransactions(Stream transactionsFileStream,
                                                       CancellationToken cancellationToken)
    {
        var text = await ReadText(transactionsFileStream, cancellationToken);
        var splitted = SplitData(text);

        return ParseTransactions(splitted.TransactionsRaw);
    }

    private async Task<string> ReadText(Stream transactionsFileStream,
                                        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(transactionsFileStream);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    private (string AccountInfoRaw, string TransactionsRaw) SplitData(string rawText)
    {
        var splitted = rawText.Split("\n\n");
        return (splitted[1], splitted[^1]);
    }

    private Transaction[] ParseTransactions(string transactionsRaw)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };

        using var reader = new StringReader(transactionsRaw);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<Transaction>().ToArray();
    }
}
