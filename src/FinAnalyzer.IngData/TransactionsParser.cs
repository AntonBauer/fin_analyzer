using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class TransactionsParser
{
    public Transaction[] ParseTransactions(string transactionsData)
    {
        var culture = CultureInfo.CreateSpecificCulture("de-DE");
        var config = new CsvConfiguration(culture)
        {
            Delimiter = ";",
        };

        using var reader = new StringReader(transactionsData);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<TransactionsMapping>();

        return csv.GetRecords<Transaction>().ToArray();
    }
}