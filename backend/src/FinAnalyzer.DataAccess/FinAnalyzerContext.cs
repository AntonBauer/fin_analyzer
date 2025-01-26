using FinAnalyzer.Domain.Models;
using FinAnalyzer.Domain.Rules;
using Microsoft.EntityFrameworkCore;

namespace FinAnalyser.DataAccess;

internal sealed class FinAnalyzerContext(DbContextOptions<FinAnalyzerContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>(); 
    public DbSet<RegexRule> RegexRules => Set<RegexRule>();
    public DbSet<Suggestion> Suggesions => Set<Suggestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
