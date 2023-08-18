using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Comment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_AuthorId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comment",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comment",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Comment",
                newName: "Text");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
