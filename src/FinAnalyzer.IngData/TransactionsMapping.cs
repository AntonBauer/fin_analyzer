using CsvHelper.Configuration;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class TransactionsMapping : ClassMap<Transaction>
{
    public TransactionsMapping()
    {
        Map(t => t.Booking).Index(0);
        Map(t => t.ValueDate).Index(1);
        Map(t => t.PayerOrPayee).Index(2);
        Map(t => t.BookingText).Index(3);
        Map(t => t.Purpose).Index(4);
        Map(t => t.Balance).Index(5);
        Map(t => t.Currency).Index(6);
        Map(t => t.Amount).Index(7);
        Map(t => t.AmountCurrency).Index(8);
    }
}