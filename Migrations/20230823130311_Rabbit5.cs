using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Rabbit5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "UserMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_AppUserId",
                table: "UserMessages",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_AppUserId",
                table: "UserMessages",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_AppUserId",
                table: "UserMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserMessages_AppUserId",
                table: "UserMessages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserMessages");
        }
    }
}
