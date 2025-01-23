using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Suggestions;

public interface ISuggestionsService
{
    Task Apply(uint suggestionId, CancellationToken cancellationToken);
    Task<Suggestion[]> ReadAll(CancellationToken cancellationToken);
}