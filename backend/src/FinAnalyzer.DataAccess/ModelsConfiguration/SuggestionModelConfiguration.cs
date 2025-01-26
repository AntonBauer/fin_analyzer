using FinAnalyzer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAnalyser.DataAccess.ModelsConfiguration;

internal sealed class SuggestionModelConfiguration : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.ToTable("suggestions");
        builder.HasKey(suggestion => suggestion.Id);

        builder.HasOne(suggesion => suggesion.Transaction)
               .WithMany();
        builder.HasOne(suggestion => suggestion.SuggestedCategory)
               .WithMany();

        builder.Navigation(suggestion => suggestion.Transaction)
               .AutoInclude();
        builder.Navigation(suggestion => suggestion.SuggestedCategory)
               .AutoInclude();
    }
}