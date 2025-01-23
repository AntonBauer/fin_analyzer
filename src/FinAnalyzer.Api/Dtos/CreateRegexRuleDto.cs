using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Api.Dtos;

internal readonly record struct CreateRegexRuleDto(string Name,
                                                   TransactionProperty PropertyToCheck,
                                                   string Expression,
                                                   uint SuggestedCategoryId);