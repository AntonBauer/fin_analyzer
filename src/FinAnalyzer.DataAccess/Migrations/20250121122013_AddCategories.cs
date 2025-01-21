using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinAnalyser.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "cathegory_id",
                table: "transactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cathegories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cathegories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactions_cathegory_id",
                table: "transactions",
                column: "cathegory_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_categories_cathegory_id",
                table: "transactions",
                column: "cathegory_id",
                principalTable: "cathegories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_categories_cathegory_id",
                table: "transactions");

            migrationBuilder.DropTable(
                name: "cathegories");

            migrationBuilder.DropIndex(
                name: "ix_transactions_cathegory_id",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "cathegory_id",
                table: "transactions");
        }
    }
}
