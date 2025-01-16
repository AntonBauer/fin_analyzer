using FinAnalyzer.IngData;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases.LoadIngData;

public static class WebApplicationExtensions
{
    public static WebApplication AddLoadIngDataEndpoints(this WebApplication app)
    {
        app.MapPost("/load-ing-data", async ([FromServices] IIngDataParser dataParser,
                                             [FromForm] IFormFile transactionsFileStream,
                                             CancellationToken cancellationToken) =>
        {
            var transactions = await dataParser.ParseTransactions(transactionsFileStream.OpenReadStream(),
                                                                   cancellationToken);
            return Results.Ok(transactions);
        });

        return app;
    }
}