using FinAnalyzer.Domain.Rules;

namespace FinAnalyser.DataAccess.Services.Rules;

public interface IRulesService
{
    Task<uint> Create(string name, TransactionProperty propertyToCheck, string expression, uint suggestedCategoryId, CancellationToken cancellationToken);
    Task<RegexRule[]> ReadAll(CancellationToken cancellationToken);
    Task Delete(uint ruleId, CancellationToken cancellationToken);
}