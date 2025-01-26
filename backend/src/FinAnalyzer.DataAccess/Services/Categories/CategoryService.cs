using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess.Services.Categories;

internal sealed class CategoryService(FinAnalyzerContext context) : ICategoryService
{
    public async Task<uint> Create(string name, CancellationToken cancellationToken)
    {
        var category = new Category { Id = 0, Name = name };
        var entry = await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }

    public async Task<Category[]> ReadAll(CancellationToken cancellationToken) =>
        await context.Categories
                     .AsNoTracking()
                     .ToArrayAsync(cancellationToken);

    public async Task Delete(uint categoryId, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([categoryId], cancellationToken);
        if (category is null) return;

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
    }
}