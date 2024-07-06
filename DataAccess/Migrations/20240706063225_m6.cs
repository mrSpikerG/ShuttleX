using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserCreatorId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserCreatorId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserCreatorId",
                table: "Chats",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserCreatorId",
                table: "Messages",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserCreatorId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserCreatorId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserCreatorId",
                table: "Chats",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserCreatorId",
                table: "Messages",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
