using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class TransactionModelConfiguration : IEntityTypeConfiguration<RawTransaction>
{
    public void Configure(EntityTypeBuilder<RawTransaction> builder)
    {
        builder.ToTable("raw_transactions");
        builder.HasKey(transaction => transaction.Id);

        builder.OwnsOne(transaction => transaction.Balance);
        builder.OwnsOne(transaction => transaction.Amount);
    }
}