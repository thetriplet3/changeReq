using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestActions_Requests_RequestId1",
                table: "RequestActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestActions",
                table: "RequestActions");

            migrationBuilder.DropIndex(
                name: "IX_RequestActions_RequestId1",
                table: "RequestActions");

            migrationBuilder.DropColumn(
                name: "RequestId1",
                table: "RequestActions");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "RequestActions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestActions",
                table: "RequestActions",
                column: "RequestActionSeq");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActions_RequestId",
                table: "RequestActions",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestActions_Requests_RequestId",
                table: "RequestActions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestActions_Requests_RequestId",
                table: "RequestActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestActions",
                table: "RequestActions");

            migrationBuilder.DropIndex(
                name: "IX_RequestActions_RequestId",
                table: "RequestActions");

            migrationBuilder.AlterColumn<string>(
                name: "RequestId",
                table: "RequestActions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestId1",
                table: "RequestActions",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestActions",
                table: "RequestActions",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActions_RequestId1",
                table: "RequestActions",
                column: "RequestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestActions_Requests_RequestId1",
                table: "RequestActions",
                column: "RequestId1",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
