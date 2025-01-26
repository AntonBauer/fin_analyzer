using FinAnalyser.DataAccess.Services.Rules;
using FinAnalyzer.Api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class RulesCrud
{
    public static WebApplication AddRulesCrudEndpoints(this WebApplication app)
    {
        app.AddCreate()
           .AddReadAll()
           .AddUpdate()
           .AddDelete();

        return app;
    }

    private static WebApplication AddCreate(this WebApplication app)
    {
        app.MapPost("/rules", async ([FromBody] CreateRegexRuleRequest dto,
                                     [FromServices] IRulesService ruleService,
                                     CancellationToken cancellationToken) =>
        {
            var ruleId = await ruleService.Create(dto.Name,
                                                  dto.PropertyToCheck,
                                                  dto.Expression,
                                                  dto.SuggestedCategoryId,
                                                  cancellationToken);
            return Results.Created($"/rules/{ruleId}", ruleId);
        }).DisableAntiforgery();

        return app;
    }

    private static WebApplication AddReadAll(this WebApplication app)
    {
        app.MapGet("/rules", async ([FromServices] IRulesService rulesService,
                                    CancellationToken cancellationToken) =>
        {
            var rules = await rulesService.ReadAll(cancellationToken);
            var dtos = rules.Select(x => new RegexRuleDto(x)).ToArray();

            return Results.Ok(dtos);
        });

        return app;
    }

    private static WebApplication AddUpdate(this WebApplication app) => app;

    private static WebApplication AddDelete(this WebApplication app)
    {
        app.MapDelete("/rules/{ruleId:int:min(1):required}", async ([FromServices] IRulesService rulesService,
                                                                    [FromRoute] uint ruleId,
                                                                    CancellationToken cancellationToken) =>
        {
            await rulesService.Delete(ruleId, cancellationToken);
            return Results.Ok();
        });

        return app;
    }
}