using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess;

internal sealed class FinAnalyzerContext(DbContextOptions<FinAnalyzerContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
