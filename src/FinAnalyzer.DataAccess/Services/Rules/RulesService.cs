using System.Text.RegularExpressions;
using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.Services.Rules;

internal sealed class RulesService(FinAnalyzerContext context) : IRulesService
{
    public async Task<uint> Create(string name,
                                   TransactionProperty propertyToCheck,
                                   string expression,
                                   uint suggestedCategoryId,
                                   CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([suggestedCategoryId], cancellationToken);
        var rule = new RegexRule
        {
            Id = 0,
            Name = name,
            PropertyToCheck = propertyToCheck,
            Expression = new Regex(expression),
            SuggestedCategory = category
        };

        var entry = await context.RegexRules.AddAsync(rule, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }

    public async Task Delete(uint ruleId, CancellationToken cancellationToken)
    {
        var rule = await context.RegexRules.FindAsync([ruleId], cancellationToken);
        if (rule is null) return;

        context.RegexRules.Remove(rule);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RegexRule[]> ReadAll(CancellationToken cancellationToken) =>
        await context.RegexRules
                     .AsNoTracking()
                     .ToArrayAsync(cancellationToken);

    public async Task<Suggestion[]> Apply(uint ruleId, CancellationToken cancellationToken)
    {
        var rule = await context.RegexRules.FindAsync([ruleId], cancellationToken);
        var transactions = await context.Transactions
                                        .Where(transaction => transaction.Cathegory == null)
                                        .ToArrayAsync(cancellationToken);

        var suggestions = transactions.Select(transaction => rule.ApplyTo(transaction))
                                      .Where(suggestion => suggestion is not null)
                                      .OfType<Suggestion>()
                                      .ToArray();

        await context.Suggesions.AddRangeAsync(suggestions, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return suggestions;
    }
}