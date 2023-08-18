using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Users_AppUserId",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Users_BlockedUsersId",
                table: "BlockedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers");

            migrationBuilder.RenameTable(
                name: "BlockedUsers",
                newName: "AppUserAppUser");

            migrationBuilder.RenameColumn(
                name: "BlockedUsersId",
                table: "AppUserAppUser",
                newName: "FollowersId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "AppUserAppUser",
                newName: "FollowedUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUsers_BlockedUsersId",
                table: "AppUserAppUser",
                newName: "IX_AppUserAppUser_FollowersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserAppUser",
                table: "AppUserAppUser",
                columns: new[] { "FollowedUsersId", "FollowersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserAppUser_Users_FollowedUsersId",
                table: "AppUserAppUser",
                column: "FollowedUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserAppUser_Users_FollowersId",
                table: "AppUserAppUser",
                column: "FollowersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserAppUser_Users_FollowedUsersId",
                table: "AppUserAppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserAppUser_Users_FollowersId",
                table: "AppUserAppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserAppUser",
                table: "AppUserAppUser");

            migrationBuilder.RenameTable(
                name: "AppUserAppUser",
                newName: "BlockedUsers");

            migrationBuilder.RenameColumn(
                name: "FollowersId",
                table: "BlockedUsers",
                newName: "BlockedUsersId");

            migrationBuilder.RenameColumn(
                name: "FollowedUsersId",
                table: "BlockedUsers",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserAppUser_FollowersId",
                table: "BlockedUsers",
                newName: "IX_BlockedUsers_BlockedUsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers",
                columns: new[] { "AppUserId", "BlockedUsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Users_AppUserId",
                table: "BlockedUsers",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Users_BlockedUsersId",
                table: "BlockedUsers",
                column: "BlockedUsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
