using FinAnalyser.DataAccess.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class ReadAccount
{
    public static WebApplication AddReadAccountEndpoints(this WebApplication app)
    {
        app.MapGet("/accounts/{accountId:guid}", async ([FromRoute] Guid accountId,
                                                        [FromServices] IAccountService accountService,
                                                        CancellationToken cancellationToken) =>
        {
            var account = await accountService.ReadAccount(accountId, cancellationToken);
            return Results.Ok(account);
        });

        return app;
    }
}