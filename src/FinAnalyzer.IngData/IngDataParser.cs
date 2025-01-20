using FinAnalyzer.IngData.Models;

namespace FinAnalyzer.IngData;

internal sealed class IngDataParser(TransactionsParser transactionsParser,
                                    AccountInfoParser accountInfoParser) : IIngDataParser
{
    public async Task<TransactionsData> ParseTransactions(Stream transactionsFileStream,
                                                       CancellationToken cancellationToken)
    {
        var (AccountInfoRaw, TransactionsRaw) = SplitData(await ReadText(transactionsFileStream, cancellationToken));

        var accountInfo = accountInfoParser.ParseAccountInfo(AccountInfoRaw);
        var transactions = await transactionsParser.ParseTransactions(TransactionsRaw);

        return new(accountInfo, transactions);
    }

    private static async Task<string> ReadText(Stream transactionsFileStream,
                                               CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(transactionsFileStream);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    private static (string AccountInfoRaw, string TransactionsRaw) SplitData(string rawText)
    {
        var splitted = rawText.Replace("\r\n", "\n").Split("\n\n");
        return (splitted[1], splitted[^1]);
    }
}
