using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserRequestAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_ProductId",
                schema: "Identity",
                table: "UserRequests",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequests_Products_ProductId",
                schema: "Identity",
                table: "UserRequests",
                column: "ProductId",
                principalSchema: "Product",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRequests_Products_ProductId",
                schema: "Identity",
                table: "UserRequests");

            migrationBuilder.DropIndex(
                name: "IX_UserRequests_ProductId",
                schema: "Identity",
                table: "UserRequests");
        }
    }
}
