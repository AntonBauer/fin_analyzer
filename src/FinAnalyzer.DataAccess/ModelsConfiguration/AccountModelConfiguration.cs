using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAnalyser.DataAccess.ModelsConfiguration;

internal sealed class AccountModelConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(account => account.Id);
        builder.OwnsOne(account => account.Info);
        builder.HasMany(account => account.Transactions)
               .WithOne();
    }
}