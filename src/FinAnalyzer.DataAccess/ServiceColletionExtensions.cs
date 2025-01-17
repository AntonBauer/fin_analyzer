using FinAnalyser.DataAccess.RawTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinAnalyser.DataAccess;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FinAnalyzerContext>(options => options.UseNpgsql(configuration.GetConnectionString("FinAnalyze"))
                                                                    .UseSnakeCaseNamingConvention())
                .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddTransient<IRawTransactionsRepository, RawTransactionsRepository>();
}