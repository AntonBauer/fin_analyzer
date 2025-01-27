using FinAnalyser.DataAccess.Services.Categories;
using FinAnalyzer.Api.Requests;
using FinAnalyzer.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinAnalyzer.Api.UseCases;

internal static class CategoriesCrud
{
    public static WebApplication AddCategoriesCrudEndpoints(this WebApplication app)
    {
        app.AddCreate()
           .AddRead()
           .AddUpdate()
           .AddDelete();

        return app;
    }

    private static WebApplication AddCreate(this WebApplication app)
    {
        app.MapPost("/categories", async ([FromBody] CreateCategoryRequest dto,
                                          [FromServices] ICategoryService categoryService,
                                          CancellationToken cancellationToken) =>
        {
            var categoryId = await categoryService.Create(dto.Name, cancellationToken);
            return Results.Created($"/categories/{categoryId}", categoryId);
        })
        .Produces<uint>()
        .DisableAntiforgery();

        return app;
    }

    private static WebApplication AddRead(this WebApplication app)
    {
        app.MapGet("/categories", async ([FromServices] ICategoryService categoryService,
                                         CancellationToken cancellationToken) =>
        {
            var categories = await categoryService.ReadAll(cancellationToken);
            return Results.Ok(categories);
        })
        .Produces<Category[]>();

        return app;
    }

    private static WebApplication AddUpdate(this WebApplication app) => app;

    private static WebApplication AddDelete(this WebApplication app)
    {
        app.MapDelete("/categories/{categoryId:int:min(1):required}", async ([FromRoute] uint categoryId,
                                                                             [FromServices] ICategoryService categoryService,
                                                                             CancellationToken cancellationToken) =>
        {
            await categoryService.Delete(categoryId, cancellationToken);
            return Results.Ok();
        });

        return app;
    }
}