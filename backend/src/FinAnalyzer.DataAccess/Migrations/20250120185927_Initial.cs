using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinAnalyser.DataAccess.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "accounts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                info_iban = table.Column<string>(type: "text", nullable: false),
                info_name = table.Column<string>(type: "text", nullable: false),
                info_account_holder = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_accounts", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "raw_transactions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                booking = table.Column<DateOnly>(type: "date", nullable: false),
                value_date = table.Column<DateOnly>(type: "date", nullable: false),
                payer_or_payee = table.Column<string>(type: "text", nullable: false),
                booking_text = table.Column<string>(type: "text", nullable: false),
                purpose = table.Column<string>(type: "text", nullable: false),
                balance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                balance_name = table.Column<int>(type: "integer", nullable: true),
                amount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                amount_name = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_raw_transactions", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "transactions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                raw_transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                account_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_transactions", x => x.id);
                table.ForeignKey(
                    name: "fk_transactions_accounts_account_id",
                    column: x => x.account_id,
                    principalTable: "accounts",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_transactions_raw_transaction_raw_transaction_id",
                    column: x => x.raw_transaction_id,
                    principalTable: "raw_transactions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_transactions_account_id",
            table: "transactions",
            column: "account_id");

        migrationBuilder.CreateIndex(
            name: "ix_transactions_raw_transaction_id",
            table: "transactions",
            column: "raw_transaction_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "transactions");

        migrationBuilder.DropTable(
            name: "accounts");

        migrationBuilder.DropTable(
            name: "raw_transactions");
    }
}
