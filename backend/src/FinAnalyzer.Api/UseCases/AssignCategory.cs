using FinAnalyser.DataAccess.Services.Transactions;
using FinAnalyzer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class AssignCategory
{
    public static WebApplication AddAssignCategoryEndpoints(this WebApplication app)
    {
        app.AssignCategoryEndpoint()
           .RemoveCategoryEndpoint();

        return app;
    }

    private static WebApplication AssignCategoryEndpoint(this WebApplication app)
    {
        app.MapPut("/accounts/{accountId:guid:required}/transactions/{transactionId:guid:required}/categories/{categoryId:int:min(1):required}/assign",
                   async ([FromRoute] Guid accountId,
                          [FromRoute] Guid transactionId,
                          [FromRoute] uint categoryId,
                          [FromServices] ITransactionsService transactionsService,
                          CancellationToken cancellationToken) =>
                   {
                       var transaction = await transactionsService.AssignCategory(accountId, transactionId, categoryId, cancellationToken);
                       return Results.Ok(transaction);
                   })
           .Produces<Transaction>();

        return app;
    }

    private static WebApplication RemoveCategoryEndpoint(this WebApplication app)
    {
        app.MapDelete("/accounts/{accountId:guid:required}/transactions/{transactionId:guid:required}/categories",
                   async ([FromRoute] Guid accountId,
                          [FromRoute] Guid transactionId,
                          [FromServices] ITransactionsService transactionsService,
                          CancellationToken cancellationToken) =>
                   {
                       var transaction = await transactionsService.RemoveCategory(accountId, transactionId, cancellationToken);
                       return Results.Ok(transaction);
                   })
           .Produces<Transaction>();

        return app;
    }
}