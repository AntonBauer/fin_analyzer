using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAnalyser.DataAccess.ModelsConfiguration;

internal sealed class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("cathegories");
        builder.HasKey(cathegory => cathegory.Id);
    }
}