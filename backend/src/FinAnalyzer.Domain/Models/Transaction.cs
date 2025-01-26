using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Domain.Models;

public sealed class Transaction
{
    public required Guid Id { get; init; }
    public required RawTransaction RawTransaction { get; init; }
    public Category? Cathegory { get; set; }

    public string GetPropertyValue(TransactionProperty property) => property switch
    {
        TransactionProperty.PayerOrPayee => RawTransaction.PayerOrPayee,
        TransactionProperty.BookingText => RawTransaction.BookingText,
        TransactionProperty.Purpose => RawTransaction.Purpose,
        _ => throw new ArgumentOutOfRangeException(nameof(property), property, null)
    };
}