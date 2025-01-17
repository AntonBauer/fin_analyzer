namespace FinAnalyzer.Domain.Models;

public record Transaction
{
    public required DateOnly Booking { get; init; }
    public required DateOnly ValueDate { get; init; }
    public required string PayerOrPayee { get; init; }
    public required string BookingText { get; init; }
    public required string Purpose { get; init; }
    public Currency Balance { get; init; }
    public Currency Amount { get; init; }
}