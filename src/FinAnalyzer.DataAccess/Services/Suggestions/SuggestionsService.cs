using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.Services.Suggestions;

internal sealed class SuggestionsService(FinAnalyzerContext context) : ISuggestionsService
{
    public async Task Apply(uint suggestionId, CancellationToken cancellationToken)
    {
        var suggesion = await context.Suggesions.FindAsync([suggestionId], cancellationToken);
        suggesion.Apply();

        context.Suggesions.Remove(suggesion);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Suggestion[]> ReadAll(CancellationToken cancellationToken) =>
        await context.Suggesions.AsNoTracking()
                                .ToArrayAsync(cancellationToken);
}