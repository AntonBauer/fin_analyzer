public readonly record struct Currency(decimal Amount, CurrencyName Name);

public enum CurrencyName
{
    Euro
}