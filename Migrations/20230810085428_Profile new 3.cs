using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Profilenew3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "UserMovies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMovies_MovieId1",
                table: "UserMovies",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId1",
                table: "UserMovies",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId1",
                table: "UserMovies");

            migrationBuilder.DropIndex(
                name: "IX_UserMovies_MovieId1",
                table: "UserMovies");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "UserMovies");
        }
    }
}
