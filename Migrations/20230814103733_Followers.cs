using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Followers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserAppUser",
                columns: table => new
                {
                    FollowedUsersId = table.Column<int>(type: "int", nullable: false),
                    FollowersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAppUser", x => new { x.FollowedUsersId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_AppUserAppUser_Users_FollowedUsersId",
                        column: x => x.FollowedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserAppUser_Users_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAppUser_FollowersId",
                table: "AppUserAppUser",
                column: "FollowersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserAppUser");
        }
    }
}
