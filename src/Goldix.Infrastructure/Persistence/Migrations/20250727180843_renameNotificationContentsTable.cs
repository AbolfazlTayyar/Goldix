using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goldix.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renameNotificationContentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationsContent_Users_SenderId",
                schema: "Notification",
                table: "NotificationsContent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_NotificationsContent_NotificationContentId",
                schema: "Notification",
                table: "UserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationsContent",
                schema: "Notification",
                table: "NotificationsContent");

            migrationBuilder.RenameTable(
                name: "NotificationsContent",
                schema: "Notification",
                newName: "NotificationContents",
                newSchema: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationsContent_SenderId",
                schema: "Notification",
                table: "NotificationContents",
                newName: "IX_NotificationContents_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationContents",
                schema: "Notification",
                table: "NotificationContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationContents_Users_SenderId",
                schema: "Notification",
                table: "NotificationContents",
                column: "SenderId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_NotificationContents_NotificationContentId",
                schema: "Notification",
                table: "UserNotifications",
                column: "NotificationContentId",
                principalSchema: "Notification",
                principalTable: "NotificationContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationContents_Users_SenderId",
                schema: "Notification",
                table: "NotificationContents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_NotificationContents_NotificationContentId",
                schema: "Notification",
                table: "UserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationContents",
                schema: "Notification",
                table: "NotificationContents");

            migrationBuilder.RenameTable(
                name: "NotificationContents",
                schema: "Notification",
                newName: "NotificationsContent",
                newSchema: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationContents_SenderId",
                schema: "Notification",
                table: "NotificationsContent",
                newName: "IX_NotificationsContent_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationsContent",
                schema: "Notification",
                table: "NotificationsContent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationsContent_Users_SenderId",
                schema: "Notification",
                table: "NotificationsContent",
                column: "SenderId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_NotificationsContent_NotificationContentId",
                schema: "Notification",
                table: "UserNotifications",
                column: "NotificationContentId",
                principalSchema: "Notification",
                principalTable: "NotificationsContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
