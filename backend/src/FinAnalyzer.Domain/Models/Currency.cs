namespace FinAnalyzer.Domain.Models;

public record Currency(decimal Amount, CurrencyName Name);

public enum CurrencyName
{
    Euro
}