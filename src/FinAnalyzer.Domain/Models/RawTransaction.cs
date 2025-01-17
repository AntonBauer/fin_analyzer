namespace FinAnalyzer.Domain.Models;

public record RawTransaction
{
    public required Guid Id { get; init; }
    public required DateOnly Booking { get; init; }
    public required DateOnly ValueDate { get; init; }
    public required string PayerOrPayee { get; init; }
    public required string BookingText { get; init; }
    public required string Purpose { get; init; }
    public Currency Balance { get; init; }
    public Currency Amount { get; init; }
}