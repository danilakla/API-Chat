using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Chat.Migrations
{
    public partial class DeleteColumnFromWhomInMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWhom",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "message_text",
                table: "Messages",
                newName: "MessageText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageText",
                table: "Messages",
                newName: "message_text");

            migrationBuilder.AddColumn<string>(
                name: "FromWhom",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
