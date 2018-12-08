using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Requests");
        }
    }
}
