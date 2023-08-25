using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class Rabbit4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessage_Users_ReceiverId",
                table: "UserMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessage_Users_SenderId",
                table: "UserMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMessage",
                table: "UserMessage");

            migrationBuilder.RenameTable(
                name: "UserMessage",
                newName: "UserMessages");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessage_SenderId",
                table: "UserMessages",
                newName: "IX_UserMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessage_ReceiverId",
                table: "UserMessages",
                newName: "IX_UserMessages_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMessages",
                table: "UserMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_ReceiverId",
                table: "UserMessages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_Users_SenderId",
                table: "UserMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_ReceiverId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_Users_SenderId",
                table: "UserMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMessages",
                table: "UserMessages");

            migrationBuilder.RenameTable(
                name: "UserMessages",
                newName: "UserMessage");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_SenderId",
                table: "UserMessage",
                newName: "IX_UserMessage_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMessages_ReceiverId",
                table: "UserMessage",
                newName: "IX_UserMessage_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMessage",
                table: "UserMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessage_Users_ReceiverId",
                table: "UserMessage",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessage_Users_SenderId",
                table: "UserMessage",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
