using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentDir",
                table: "Requests",
                newName: "FormPath");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "FormPath",
                table: "Requests",
                newName: "AttachmentDir");
        }
    }
}
