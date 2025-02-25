using FinAnalyzer.IngData;

namespace FinAnalyzer.Api.UseCases.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        return services.AddIngData();
    }
}