using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.Domain.Rules;

public interface IRule
{
    Suggestion? ApplyTo(Transaction transaction);
}