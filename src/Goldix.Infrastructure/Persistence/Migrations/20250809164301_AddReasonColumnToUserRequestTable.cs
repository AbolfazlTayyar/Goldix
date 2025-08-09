using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonColumnToUserRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                schema: "Identity",
                table: "UserRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                schema: "Identity",
                table: "UserRequests");
        }
    }
}
