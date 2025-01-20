using FinAnalyser.DataAccess.AccountServices;
using FinAnalyzer.IngData;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases.UploadIngData;

public static class UploadIngDataEndpoints
{
    public static WebApplication AddLoadIngDataEndpoints(this WebApplication app)
    {
        app.MapPost("/upload-ing-data", async ([FromServices] IIngDataParser dataParser,
                                               [FromServices] IAccountService accountService,
                                               IFormFile transactionsFile,
                                               CancellationToken cancellationToken) =>
        {
            var transactionsData = await dataParser.ParseTransactions(transactionsFile.OpenReadStream(),
                                                                      cancellationToken);

            var accountId = await accountService.SaveTransactions(transactionsData.AccountInfo,
                                                                  transactionsData.Transactions,
                                                                  cancellationToken);

            return Results.Created($"/accounts/{accountId}", accountId);
        })
        .DisableAntiforgery();

        return app;
    }
}