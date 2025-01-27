using FinAnalyser.DataAccess.Services.Accounts;
using FinAnalyzer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class ReadAccount
{
    public static WebApplication AddReadAccountEndpoints(this WebApplication app)
    {
        app.MapGet("/accounts/{accountId:guid:required}", async ([FromRoute] Guid accountId,
                                                                 [FromServices] IAccountService accountService,
                                                                 CancellationToken cancellationToken) =>
        {
            var account = await accountService.ReadAccount(accountId, cancellationToken);
            return Results.Ok(account);
        })
        .Produces<Account>();

        return app;
    }
}