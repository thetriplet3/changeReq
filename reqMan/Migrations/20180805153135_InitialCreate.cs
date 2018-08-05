using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reqMan.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "RequestActionSequence",
                schema: "shared",
                startValue: 100000L);

            migrationBuilder.CreateSequence<int>(
                name: "RequestTypeSequence",
                schema: "shared",
                startValue: 100000L);

            migrationBuilder.CreateTable(
                name: "RequestType",
                columns: table => new
                {
                    RequestTypeSeq = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR shared.RequestTypeSequence"),
                    RequestTypeId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestType", x => x.RequestTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<string>(nullable: false),
                    RequestTypeId = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Request_RequestType_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestType",
                        principalColumn: "RequestTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestActions",
                columns: table => new
                {
                    RequestActionSeq = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR shared.RequestActionSequence"),
                    RequestId = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    RequestId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestActions", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_RequestActions_Request_RequestId1",
                        column: x => x.RequestId1,
                        principalTable: "Request",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestActions_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestTypeId",
                table: "Request",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Username",
                table: "Request",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActions_RequestId1",
                table: "RequestActions",
                column: "RequestId1");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActions_Username",
                table: "RequestActions",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestActions");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "RequestType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "RequestActionSequence",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "RequestTypeSequence",
                schema: "shared");
        }
    }
}
