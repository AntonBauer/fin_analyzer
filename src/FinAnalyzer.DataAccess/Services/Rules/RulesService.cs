using FinAnalyzer.Domain.Rules;

namespace FinAnalyser.DataAccess.Services.Rules;

internal sealed class RulesService : IRulesService
{
    public Task<uint> Create(TransactionProperty propertyToCheck, string expression, uint suggestedCategoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(uint ruleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RegexRule[]> ReadAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}