using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Chat.Migrations
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToWhom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromWhom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactsConversations",
                columns: table => new
                {
                    ContactsId = table.Column<int>(type: "int", nullable: false),
                    ConversationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsConversations", x => new { x.ContactsId, x.ConversationsId });
                    table.ForeignKey(
                        name: "FK_ContactsConversations_Contacts_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsConversations_Conversations_ConversationsId",
                        column: x => x.ConversationsId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromWhom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationsId",
                        column: x => x.ConversationsId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactsId",
                table: "Contacts",
                column: "ContactsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsConversations_ConversationsId",
                table: "ContactsConversations",
                column: "ConversationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationsId",
                table: "Messages",
                column: "ConversationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContactsId",
                table: "Notifications",
                column: "ContactsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactsConversations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
