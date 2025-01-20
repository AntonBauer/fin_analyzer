using FinAnalyser.DataAccess.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class ReadAccountTransactions
{
    public static WebApplication AddReadAccountTransactionsEndpoints(this WebApplication app)
    {
        app.MapGet("/accounts/{accountId:guid}/transactions", async ([FromRoute] Guid accountId,
                                                                     [FromQuery] DateOnly? from,
                                                                     [FromQuery] DateOnly? to,
                                                                     [FromServices] IAccountService accountService,
                                                                     CancellationToken cancellationToken) =>
        {
            var account = await accountService.ReadAccount(accountId, cancellationToken);
            return Results.Ok(account);
        });

        return app;
    }
}