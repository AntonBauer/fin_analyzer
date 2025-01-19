namespace FinAnalyzer.Domain.Models;

public readonly record struct AccountInfo(string Iban,
                                          string Name,
                                          string AccountHolder);