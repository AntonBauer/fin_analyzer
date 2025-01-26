using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData.Models;

public readonly record struct TransactionsData(AccountInfo AccountInfo,
                                               Transaction[] Transactions);