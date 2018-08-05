using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class R02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_RequestType_RequestTypeId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Users_Username",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestActions_Request_RequestId1",
                table: "RequestActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_Request_Username",
                table: "Requests",
                newName: "IX_Requests_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Request_RequestTypeId",
                table: "Requests",
                newName: "IX_Requests_RequestTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestActions_Requests_RequestId1",
                table: "RequestActions",
                column: "RequestId1",
                principalTable: "Requests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestType_RequestTypeId",
                table: "Requests",
                column: "RequestTypeId",
                principalTable: "RequestType",
                principalColumn: "RequestTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_Username",
                table: "Requests",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestActions_Requests_RequestId1",
                table: "RequestActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestType_RequestTypeId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_Username",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_Username",
                table: "Request",
                newName: "IX_Request_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_RequestTypeId",
                table: "Request",
                newName: "IX_Request_RequestTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_RequestType_RequestTypeId",
                table: "Request",
                column: "RequestTypeId",
                principalTable: "RequestType",
                principalColumn: "RequestTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Users_Username",
                table: "Request",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestActions_Request_RequestId1",
                table: "RequestActions",
                column: "RequestId1",
                principalTable: "Request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
