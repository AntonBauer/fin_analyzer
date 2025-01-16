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

    private static Transaction[] ParseTransactions(string transactionsRaw)
    {
        var culture = CultureInfo.CreateSpecificCulture("de-DE");
        var config = new CsvConfiguration(culture)
        {
            Delimiter = ";",
        };

        using var reader = new StringReader(transactionsRaw);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<TransactionsMapping>();

        return csv.GetRecords<Transaction>().ToArray();
    }
}
