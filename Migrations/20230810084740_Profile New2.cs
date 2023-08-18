using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class ProfileNew2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_Movies_MovieId",
                table: "UserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_Users_AppUserId",
                table: "UserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie");

            migrationBuilder.RenameTable(
                name: "UserMovie",
                newName: "UserMovies");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovies",
                newName: "IX_UserMovies_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovie_AppUserId",
                table: "UserMovies",
                newName: "IX_UserMovies_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Users_AppUserId",
                table: "UserMovies",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Users_AppUserId",
                table: "UserMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies");

            migrationBuilder.RenameTable(
                name: "UserMovies",
                newName: "UserMovie");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_MovieId",
                table: "UserMovie",
                newName: "IX_UserMovie_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_AppUserId",
                table: "UserMovie",
                newName: "IX_UserMovie_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_Movies_MovieId",
                table: "UserMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_Users_AppUserId",
                table: "UserMovie",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
