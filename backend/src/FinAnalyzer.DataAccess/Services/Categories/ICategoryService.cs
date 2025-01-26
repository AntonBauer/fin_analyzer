using FinAnalyzer.Domain.Models;

namespace FinAnalyser.DataAccess.Services.Categories;

public interface ICategoryService
{
    Task<uint> Create(string name, CancellationToken cancellationToken);
    Task<Category[]> ReadAll(CancellationToken cancellationToken);
    Task Delete(uint categoryId, CancellationToken cancellationToken);
}