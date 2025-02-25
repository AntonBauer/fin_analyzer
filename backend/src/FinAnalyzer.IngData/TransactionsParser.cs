using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class TransactionsParser
{
    public async Task<Transaction[]> ParseTransactions(string transactionsData)
    {
        using var csv = CreateReader(transactionsData);
        await csv.ReadAsync();
        csv.ReadHeader();

        var rawTransactions = new List<RawTransaction>();

        while (await csv.ReadAsync())
        {
            var transaction = ReadTransaction(csv);
            rawTransactions.Add(transaction);
        }

        return [.. rawTransactions.Select(raw => new Transaction { Id = Guid.NewGuid(), RawTransaction = raw })];
    }

    private static CsvReader CreateReader(string transactionsData)
    {
        var culture = CultureInfo.CreateSpecificCulture("de-DE");
        var config = new CsvConfiguration(culture)
        {
            Delimiter = ";",
        };

        var reader = new StringReader(transactionsData);
        return new CsvReader(reader, config);
    }

    private static RawTransaction ReadTransaction(CsvReader csv)
    {
        var booking = csv.GetField<DateOnly>(0);
        var valueDate = csv.GetField<DateOnly>(1);
        var payerOrPayee = csv.GetField<string>(2);
        var bookingText = csv.GetField<string>(3);
        var purpose = csv.GetField<string>(4);

        // ToDo: parse currency code
        var balanceAmount = csv.GetField<decimal>(5);
        var balanceCurrency = csv.GetField<string>(6);
        var balance = new Currency(balanceAmount, CurrencyName.Euro);

        var amount = csv.GetField<decimal>(7);
        var amountCurrency = csv.GetField<string>(8);
        var tmp = new Currency(amount, CurrencyName.Euro);

        return new RawTransaction
        {
            Id = Guid.NewGuid(),
            Booking = booking,
            ValueDate = valueDate,
            PayerOrPayee = payerOrPayee ?? string.Empty,
            BookingText = bookingText ?? string.Empty,
            Purpose = purpose ?? string.Empty,
            Balance = balance,
            Amount = tmp
        };
    }
}