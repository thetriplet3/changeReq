using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CyclePartnerList",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CyclePartnerList",
                table: "Requests");
        }
    }
}
