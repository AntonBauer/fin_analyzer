using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinAnalyser.DataAccess.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FinAnalyzerContext>();
        await context.Database.MigrateAsync();
    }
}