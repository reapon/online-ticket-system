using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class agent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8eca6e6b-7974-4c1c-b16d-8989ec2fb073");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "3d68543a-1071-444e-902c-59aaab6ad554");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7c553365-04bc-4a96-b3a9-0a23d62f4213");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-421f-86af-483d92fd7210", "99cc086d-dfe2-4f87-866b-08622245ec34", "Agent", "AGENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d5038d8-8914-427f-b0f4-98defa7efb94", "AQAAAAEAACcQAAAAECJCbjwc7VwWf76hD6tZ/nuTOc4akksZoQIiUBpTZGfGBDEV+m21TOkKek9qaPXZQA==", "d3e2f62d-b34c-4472-b767-ac4a0273ca77" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d92fd7210");

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
    }
}
