using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class com : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "UserMails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "TicketMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "GroupUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComID",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyInformations",
                columns: table => new
                {
                    ComID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInformations", x => x.ComID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "12d044b1-02af-4af4-a6d8-9986834cc726");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "19f5ce90-a937-4fc7-b67d-61500cb0fbdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d92fd7210",
                column: "ConcurrencyStamp",
                value: "7722c2ca-d0c8-414a-b868-3e3973b16b57");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5fb063fd-69dc-4c5a-81e7-3d5435199f21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-421f-86af-493d06fd7210", "46fb2acd-58b8-47be-acd1-4616d03489e8", "Company Admin", "COMPANY ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75101d8c-85b1-4006-98bb-2b1713b5a5a3", "AQAAAAEAACcQAAAAEDuM5CMwiE4LLBE73HFvkoK2fWDs3SRq8SztsW8y6o8y7RpMACay+4JjSOeF1qfzPw==", "401bd51a-cf2d-4660-b428-10c805e64efd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInformations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-493d06fd7210");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "UserMails");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ComID",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "ab5c26c6-6ff7-4ad8-ad04-c8c20d0b0669");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "4fadee14-11eb-4710-8a00-52ff1ec3546f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d92fd7210",
                column: "ConcurrencyStamp",
                value: "c171c456-9ee8-4029-8427-358289e88839");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b2331c34-4882-4b39-811a-5edcfaab8340");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4f46484-7f52-42b1-be9a-b6be13d6710f", "AQAAAAEAACcQAAAAEDPRgpNjUfm4YV06zpJYSYngqODetRjrijDOZZllSyYuuWXOo3uOpkpj+pczebCYTw==", "5da1016d-e898-4a43-81e9-2afb624d6ea6" });
        }
    }
}
