using FinAnalyser.DataAccess.RawTransactions;
using FinAnalyzer.IngData;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases.LoadIngData;

public static class WebApplicationExtensions
{
    public static WebApplication AddLoadIngDataEndpoints(this WebApplication app)
    {
        app.MapPost("/load-ing-data", async ([FromServices] IIngDataParser dataParser,
                                             [FromServices] IRawTransactionsRepository rawTransactionsRepository,
                                             IFormFile transactionsFile,
                                             CancellationToken cancellationToken) =>
        {
            var transactions = await dataParser.ParseTransactions(transactionsFile.OpenReadStream(),
                                                                  cancellationToken);
            await rawTransactionsRepository.Save(transactions, cancellationToken);

            return Results.Ok(transactions);
        })
        .DisableAntiforgery();

        return app;
    }
}