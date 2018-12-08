using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Attachment",
                table: "Requests",
                newName: "AttachmentDir");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentDir",
                table: "Requests",
                newName: "Attachment");
        }
    }
}
