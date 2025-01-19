using FinAnalyzer.Domain.Models;

namespace FinAnalyzer.IngData;

internal sealed class AccountInfoParser
{
    public AccountInfo ParseAccountInfo(string rawAccountInfo)
    {
        var (rawIban, rawName, rawAccountHolder) = Split(rawAccountInfo);
        return new(ExtractValue(rawIban), ExtractValue(rawName), ExtractValue(rawAccountHolder));
    }

    private static (string RawIban, string RawName, string RawAccountHolder) Split(string rawAccountInfo)
    {
        var dataRows = rawAccountInfo.Split('\n');
        return (dataRows[0], dataRows[1], dataRows[3]);
    }

    private static string ExtractValue(string rawData) => rawData.Split(';')[1].Trim();
}