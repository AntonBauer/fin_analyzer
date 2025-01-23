using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinAnalyser.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddRegexRule : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "regex_rules",
            columns: table => new
            {
                id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                expression = table.Column<string>(type: "text", nullable: false),
                property_to_check = table.Column<int>(type: "integer", nullable: false),
                suggested_category_id = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_regex_rules", x => x.id);
                table.ForeignKey(
                    name: "fk_regex_rules_categories_suggested_category_id",
                    column: x => x.suggested_category_id,
                    principalTable: "cathegories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_regex_rules_name",
            table: "regex_rules",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_regex_rules_suggested_category_id",
            table: "regex_rules",
            column: "suggested_category_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "regex_rules");
    }
}
