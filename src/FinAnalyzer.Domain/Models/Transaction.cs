namespace FinAnalyzer.Domain.Models;

public record Transaction
{
    public required string Booking { get; init; }
    public required string ValueDate { get; init; }
    public required string PayerOrPayee { get; init; }
    public required string BookingText { get; init; }
    public required string Purpose { get; init; }
    public decimal Balance { get; init; }
    public required string Currency { get; init; }
    public decimal Amount { get; init; }
    public required string AmountCurrency { get; init; }
}