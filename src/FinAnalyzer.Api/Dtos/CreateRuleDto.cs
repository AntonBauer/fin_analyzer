using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Api.Dtos;

internal readonly record struct CreateRuleDto(TransactionProperty PropertyToCheck,
                                              string Expression,
                                              uint SuggestedCategoryId);