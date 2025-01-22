using System.Text.RegularExpressions;
using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.Domain.Rules;

public sealed class RegexRule : IRule
{
    public required Regex Expression { get; init; }

    public required TransactionProperty PropertyToCheck { get; init; }

    public required Category SuggestedCategory { get; init; }

    public Suggestion? ApplyTo(Transaction transaction)
    {
        var propValue = transaction.GetPropertyValue(PropertyToCheck);

        return Expression.IsMatch(propValue)
            ? new()
            {
                Id = Guid.NewGuid(),
                Transaction = transaction,
                SuggestedCategory = SuggestedCategory
            }
            : default;
    }
}