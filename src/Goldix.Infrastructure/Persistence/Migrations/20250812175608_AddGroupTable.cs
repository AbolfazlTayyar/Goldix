using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyPriceDifferencePercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SellPriceDifferencePercent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                schema: "Identity",
                table: "Users",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_GroupId",
                schema: "Identity",
                table: "Users",
                column: "GroupId",
                principalSchema: "Identity",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_GroupId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupId",
                schema: "Identity",
                table: "Users");
        }
    }
}
