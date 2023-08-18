using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 8 WHERE Id = 1;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 2;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 3;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 4;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 5;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 6;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 7;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 8;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 9;");

            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 10;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 11;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 12;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 13;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 14;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 15;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 16;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 17;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 18;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 19;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 20;");
            migrationBuilder.Sql("UPDATE Movies SET CategoryId = 9 WHERE Id = 21;");
            // Repeat the above line for each movie, setting the correct CategoryId value based on its Id
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
