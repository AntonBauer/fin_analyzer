using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;

internal readonly record struct RegexRuleDto
{
    public uint Id { get; init; }
    public string Name { get; init; }
    public string Expression { get; init; }
    public TransactionProperty PropertyToChec { get; init; }
    public Category SuggestedCatogory { get; init; }

    public RegexRuleDto(RegexRule regexRule)
    {
        Id = regexRule.Id;
        Name = regexRule.Name;
        Expression = regexRule.Expression.ToString();
        PropertyToChec = regexRule.PropertyToCheck;
        SuggestedCatogory = regexRule.SuggestedCategory;
    }
}