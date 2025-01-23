using System.Text.RegularExpressions;
using FinAnalyzer.Domain.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinAnalyser.DataAccess.ModelsConfiguration;

internal sealed class RegexRuleModelConfiguration : IEntityTypeConfiguration<RegexRule>
{
    public void Configure(EntityTypeBuilder<RegexRule> builder)
    {
        builder.ToTable("regex_rules");

        builder.HasKey(rule => rule.Id);
        builder.HasIndex(rule => rule.Name)
               .IsUnique();

        builder.HasOne(rule => rule.SuggestedCategory)
               .WithMany();
        builder.Navigation(rule => rule.SuggestedCategory)
               .AutoInclude();

        builder.Property(rule => rule.Expression)
               .HasConversion(
                   expression => expression.ToString(),
                   expression => new Regex(expression)
               );
    }
}