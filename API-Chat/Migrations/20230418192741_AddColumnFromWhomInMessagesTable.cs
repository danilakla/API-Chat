using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Chat.Migrations
{
    public partial class AddColumnFromWhomInMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromWhom",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWhom",
                table: "Messages");
        }
    }
}
