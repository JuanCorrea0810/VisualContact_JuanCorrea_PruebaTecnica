using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHook_Notifications.Migrations
{
    /// <inheritdoc />
    public partial class FixingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payer_Transaction_TransactionId",
                table: "Payer");

            migrationBuilder.DropIndex(
                name: "IX_Payer_TransactionId",
                table: "Payer");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Payer");

            migrationBuilder.AddColumn<string>(
                name: "PayerId",
                table: "Transaction",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PayerId",
                table: "Transaction",
                column: "PayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Payer_PayerId",
                table: "Transaction",
                column: "PayerId",
                principalTable: "Payer",
                principalColumn: "Document");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Payer_PayerId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PayerId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "PayerId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Payer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payer_TransactionId",
                table: "Payer",
                column: "TransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payer_Transaction_TransactionId",
                table: "Payer",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
