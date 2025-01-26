using FinAnalyser.DataAccess.Services.Accounts;
using FinAnalyser.DataAccess.Services.Categories;
using FinAnalyser.DataAccess.Services.Rules;
using FinAnalyser.DataAccess.Services.Suggestions;
using FinAnalyser.DataAccess.Services.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinAnalyser.DataAccess.Extensions;

public static class ServiceColletionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services,
                                                   IConfiguration configuration)
    {
        services.AddDbContext<FinAnalyzerContext>(options => options.UseNpgsql(configuration.GetConnectionString("FinAnalyzer"))
                                                                    .UseSnakeCaseNamingConvention())
                .AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<IAccountService, AccountService>()
                .AddTransient<ITransactionsService, TransactionsService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IRulesService, RulesService>()
                .AddTransient<ISuggestionsService, SuggestionsService>();
}