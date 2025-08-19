using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminIdToWalletTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                schema: "Wallet",
                table: "WalletTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_AdminId",
                schema: "Wallet",
                table: "WalletTransactions",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Users_AdminId",
                schema: "Wallet",
                table: "WalletTransactions",
                column: "AdminId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Users_AdminId",
                schema: "Wallet",
                table: "WalletTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WalletTransactions_AdminId",
                schema: "Wallet",
                table: "WalletTransactions");

            migrationBuilder.DropColumn(
                name: "AdminId",
                schema: "Wallet",
                table: "WalletTransactions");
        }
    }
}
