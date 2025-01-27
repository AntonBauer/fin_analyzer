using FinAnalyser.DataAccess.Services.Rules;
using FinAnalyser.DataAccess.Services.Suggestions;
using FinAnalyzer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class Rules
{
    public static WebApplication AddRulesApplicationEndpoints(this WebApplication app)
    {
        return app.ApplyRule()
                  .ReadSuggestions()
                  .ApplySuggestion();
    }

    private static WebApplication ApplyRule(this WebApplication app)
    {
        app.MapPost("/rules/{ruleId:int:min(1):required}/apply",
                    async ([FromRoute] uint ruleId,
                           [FromServices] IRulesService rulesService,
                           CancellationToken cancellationToken) =>
                    {
                        var suggestions = await rulesService.Apply(ruleId, cancellationToken);
                        return Results.Created("/suggestions", suggestions);
                    })
           .DisableAntiforgery()
           .Produces<Suggestion[]>();
        return app;
    }

    private static WebApplication ReadSuggestions(this WebApplication app)
    {
        app.MapGet("/suggestions", async ([FromServices] ISuggestionsService suggestionsService,
                                          CancellationToken cancellationToken) =>
        {
            var suggestions = await suggestionsService.ReadAll(cancellationToken);
            return Results.Ok(suggestions);
        })
        .Produces<Suggestion[]>();
        return app;
    }

    private static WebApplication ApplySuggestion(this WebApplication app)
    {
        app.MapPost("/suggestions/{suggestionId:guid:required}/apply",
                    async ([FromRoute] Guid suggestionId,
                           [FromServices] ISuggestionsService suggestionsService,
                           CancellationToken cancellationToken) =>
                    {
                        await suggestionsService.Apply(suggestionId, cancellationToken);
                        return Results.Ok();
                    }).DisableAntiforgery();

        return app;
    }
}