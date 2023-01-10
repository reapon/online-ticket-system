using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class nnsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Groups_GroupID1",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupID1",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupID1",
                table: "Groups");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupID1",
                table: "Groups",
                type: "int",
                nullable: true);

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
                keyValue: "2c5e174e-3b0e-421f-86af-483d92fd7210",
                column: "ConcurrencyStamp",
                value: "99cc086d-dfe2-4f87-866b-08622245ec34");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7c553365-04bc-4a96-b3a9-0a23d62f4213");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d5038d8-8914-427f-b0f4-98defa7efb94", "AQAAAAEAACcQAAAAECJCbjwc7VwWf76hD6tZ/nuTOc4akksZoQIiUBpTZGfGBDEV+m21TOkKek9qaPXZQA==", "d3e2f62d-b34c-4472-b767-ac4a0273ca77" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupID1",
                table: "Groups",
                column: "GroupID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Groups_GroupID1",
                table: "Groups",
                column: "GroupID1",
                principalTable: "Groups",
                principalColumn: "GroupID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
