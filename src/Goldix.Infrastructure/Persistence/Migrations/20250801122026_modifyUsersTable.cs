using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifyUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                schema: "identity",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "identity",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "در انتظار تایید");

            migrationBuilder.AddColumn<decimal>(
                name: "WalletBalance",
                schema: "identity",
                table: "Users",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletBalance",
                schema: "identity",
                table: "Users");
        }
    }
}
