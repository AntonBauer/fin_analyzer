using Microsoft.Extensions.DependencyInjection;

namespace FinAnalyzer.IngData;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngData(this IServiceCollection services) =>
        services.AddTransient<IIngDataParser, IngDataParser>();
}