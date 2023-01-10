using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class hhasdfasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5f3609e1-3db4-4d20-bf16-8422a473ff5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "3ecb7117-1d95-4695-8fc7-f4532581899d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d92fd7210",
                column: "ConcurrencyStamp",
                value: "3248550a-6190-4717-8d82-055773dd1e4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-493d06fd7210",
                column: "ConcurrencyStamp",
                value: "2fc2a17f-977b-4012-a897-fed2d768cc44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3cf1db1e-cd0f-40ad-b871-bb722769fcfc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfcbcf92-199b-4d5c-9213-79230e11446f", "AQAAAAEAACcQAAAAEH9ga/VgxZIcV9+Y9yUWdvq4vlX81/3oV+OXfh05ZF/DpK7on118onrHCMDEyRplgw==", "e742194e-fd05-4a9f-a301-ff69dfa8143c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: "2c5e174e-3b0e-421f-86af-493d06fd7210",
                column: "ConcurrencyStamp",
                value: "46fb2acd-58b8-47be-acd1-4616d03489e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5fb063fd-69dc-4c5a-81e7-3d5435199f21");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75101d8c-85b1-4006-98bb-2b1713b5a5a3", "AQAAAAEAACcQAAAAEDuM5CMwiE4LLBE73HFvkoK2fWDs3SRq8SztsW8y6o8y7RpMACay+4JjSOeF1qfzPw==", "401bd51a-cf2d-4660-b428-10c805e64efd" });
        }
    }
}
