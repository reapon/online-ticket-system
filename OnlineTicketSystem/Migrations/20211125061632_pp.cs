using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class pp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d1c3c22a-7ef8-40b3-86d2-62f0f0cd14d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "f1c59e00-15c7-45f4-b8fa-f10dfb969940");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "d183fd3b-1fa2-4777-9bf6-f5034379fd62");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3412f06a-47e5-4383-b997-e5db7dee6e07", "AQAAAAEAACcQAAAAENYGyW67WGsNABXSNsTurQbAlyWqvdPQOYHljhP1IqkKV8uO8vII1oAcVqn7eON4EQ==", "3ac199fc-0fe7-4bfb-9d0f-dabd37c0283b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "0047a41c-f87e-4ee2-a7c7-6c83d0faf4f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "28e68f35-19a4-4517-98e5-bc5343c97a60");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b99c549e-527d-47ef-be39-76fd91c5401c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52ec1e9a-fa97-4d1e-abde-3d45fac5bec8", "AQAAAAEAACcQAAAAEKwPcXHRb74Pl/Yt6mcKTRx0aXtBS7Go80Jg8C03ClCY2vS/kdUAabTeBo/JIqKvUQ==", "6b38b3d8-a87a-4af5-8c75-f25e9b544c70" });
        }
    }
}
