using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAnalyser.DataAccess.ModelsConfiguration;

internal sealed class TransactionModelConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(transaction => transaction.Id);

        builder.HasOne(transaction => transaction.RawTransaction)
               .WithMany();

        builder.HasOne(transaction => transaction.Cathegory)
               .WithMany();

        builder.Navigation(transaction => transaction.RawTransaction).AutoInclude();
        builder.Navigation(transaction => transaction.Cathegory).AutoInclude();
    }
}