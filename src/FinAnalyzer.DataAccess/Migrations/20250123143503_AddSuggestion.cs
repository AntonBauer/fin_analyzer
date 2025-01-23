using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAnalyser.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddSuggestion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "suggestions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                suggested_category_id = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_suggestions", x => x.id);
                table.ForeignKey(
                    name: "fk_suggestions_cathegories_suggested_category_id",
                    column: x => x.suggested_category_id,
                    principalTable: "cathegories",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_suggestions_transactions_transaction_id",
                    column: x => x.transaction_id,
                    principalTable: "transactions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_suggestions_suggested_category_id",
            table: "suggestions",
            column: "suggested_category_id");

        migrationBuilder.CreateIndex(
            name: "ix_suggestions_transaction_id",
            table: "suggestions",
            column: "transaction_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "suggestions");
    }
}
