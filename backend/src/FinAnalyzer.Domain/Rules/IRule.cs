using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.Domain.Rules;

public interface IRule
{
    string Name { get; }

    Suggestion? ApplyTo(Transaction transaction);
}