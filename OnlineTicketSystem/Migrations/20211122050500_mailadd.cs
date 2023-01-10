using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class mailadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailList = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMails", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "746af3c9-573c-4860-b1b5-8899f021be06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "c3c56e5c-7ca8-4acf-91a3-ca559778e43f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0a8a9ab9-52fa-472b-8791-fe739a45754a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c053201f-bc0b-484a-90d2-ca9515fa1f42", "AQAAAAEAACcQAAAAEEZBBSZBTZC2nU0aOUBumyJT/tUZo7AXDTPVT/dXHDOgQ5tuZV5yMxeA3c7YiFSWng==", "f2980164-c009-484a-bea2-3ade3cd57d31" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "fcf81d49-7379-4889-b939-675560851daf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "479e6695-0c4d-4fa6-b6bf-513e4ed663d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "79216fd8-ffaf-4768-ada4-03ec69916019");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63c46197-8f0d-4725-ae6e-2708b276d282", "AQAAAAEAACcQAAAAEFt39FxH2B3ezZrSA8iIWzlxJSl3C7NVKyxNiRO9SJ4xQvWJZma7kgchqtA43gUssg==", "db36cd99-d421-4ca5-ac40-6744c6b67503" });
        }
    }
}
