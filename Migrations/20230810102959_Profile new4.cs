using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Profilenew4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteMovie",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteMovie", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteMovie_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchedMovie",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchedMovie", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteMovie_MovieId",
                table: "UserFavoriteMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchedMovie_MovieId",
                table: "UserWatchedMovie",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteMovie");

            migrationBuilder.DropTable(
                name: "UserWatchedMovie");
        }
    }
}
