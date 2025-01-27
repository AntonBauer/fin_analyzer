using System.Transactions;
using FinAnalyser.DataAccess.Services.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class ReadAccountTransactions
{
    public static WebApplication AddReadAccountTransactionsEndpoints(this WebApplication app)
    {
        app.MapGet("/accounts/{accountId:guid:required}/transactions", async ([FromRoute] Guid accountId,
                                                                              [FromQuery] DateOnly? from,
                                                                              [FromQuery] DateOnly? to,
                                                                              [FromServices] ITransactionsService transactionsService,
                                                                              CancellationToken cancellationToken) =>
        {
            var transactions = await transactionsService.GetTransactions(accountId, from, to, cancellationToken);
            return Results.Ok(transactions);
        })
        .Produces<Transaction[]>();

        return app;
    }
}