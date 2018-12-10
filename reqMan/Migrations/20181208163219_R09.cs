using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PensionPerecentage",
                table: "Requests",
                newName: "RevisedPensionPerecentage");

            migrationBuilder.AddColumn<float>(
                name: "CurrentAmount",
                table: "Requests",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CurrentPensionPerecentage",
                table: "Requests",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RevisedAmount",
                table: "Requests",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAmount",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CurrentPensionPerecentage",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RevisedAmount",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RevisedPensionPerecentage",
                table: "Requests",
                newName: "PensionPerecentage");
        }
    }
}
