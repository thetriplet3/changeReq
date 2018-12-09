using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormPath",
                table: "Requests");

            migrationBuilder.AddColumn<string>(
                name: "FormPath",
                table: "RequestType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormPath",
                table: "RequestType");

            migrationBuilder.AddColumn<string>(
                name: "FormPath",
                table: "Requests",
                nullable: true);
        }
    }
}
