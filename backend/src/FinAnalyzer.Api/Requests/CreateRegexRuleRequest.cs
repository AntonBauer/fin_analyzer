using FinAnalyzer.Domain.Rules;

namespace FinAnalyzer.Api.Requests;

internal readonly record struct CreateRegexRuleRequest(string Name,
                                                       TransactionProperty PropertyToCheck,
                                                       string Expression,
                                                       uint SuggestedCategoryId);