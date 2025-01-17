using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess;

internal sealed class FinAnalyzerContext : DbContext
{
    public DbSet<RawTransaction> RawTransactions => Set<RawTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
