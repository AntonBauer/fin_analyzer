using FinAnalyser.DataAccess.AccountServices;
using FinAnalyzer.IngData;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases.LoadIngData;

public static class WebApplicationExtensions
{
    public static WebApplication AddLoadIngDataEndpoints(this WebApplication app)
    {
        app.MapPost("/load-ing-data", async ([FromServices] IIngDataParser dataParser,
                                             [FromServices] IAccountService accountService,
                                             IFormFile transactionsFile,
                                             CancellationToken cancellationToken) =>
        {
            var transactionsData = await dataParser.ParseTransactions(transactionsFile.OpenReadStream(),
                                                                      cancellationToken);

            await accountService.SaveTransactions(transactionsData.AccountInfo,
                                                  transactionsData.Transactions,
                                                  cancellationToken);

            return Results.Ok();
        })
        .DisableAntiforgery();

        return app;
    }
}