using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Suggestions;

public interface ISuggestionsService
{
    Task Apply(Guid suggestionId, CancellationToken cancellationToken);
    Task<Suggestion[]> ReadAll(CancellationToken cancellationToken);
}