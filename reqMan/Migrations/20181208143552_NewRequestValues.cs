using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class NewRequestValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CycleSchemeRequest",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OptOut",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "PensionPerecentage",
                table: "Requests",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TasteCardLink",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoneFrom",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoneTo",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CycleSchemeRequest",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "OptOut",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PensionPerecentage",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TasteCardLink",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ZoneFrom",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ZoneTo",
                table: "Requests");
        }
    }
}
