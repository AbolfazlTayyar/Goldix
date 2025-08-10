using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeDateTimeColumnsNameConsistent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "Identity",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                schema: "Product",
                table: "Products",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "Product",
                table: "Products",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Identity",
                table: "Users",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                schema: "Product",
                table: "Products",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Product",
                table: "Products",
                newName: "CreateDate");
        }
    }
}
