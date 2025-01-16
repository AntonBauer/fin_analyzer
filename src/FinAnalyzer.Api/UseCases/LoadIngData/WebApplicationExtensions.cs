using FinAnalyzer.IngData;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases.LoadIngData;

public static class WebApplicationExtensions
{
    public static WebApplication AddLoadIngDataEndpoints(this WebApplication app)
    {
        app.MapPost("/load-ing-data", async ([FromServices] IIngDataParser dataParser,
                                             IFormFile transactionsFile,
                                             CancellationToken cancellationToken) =>
        {
            var transactions = await dataParser.ParseTransactions(transactionsFile.OpenReadStream(),
                                                                  cancellationToken);
            return Results.Ok(transactions);
        })
        .DisableAntiforgery();

        return app;
    }
}