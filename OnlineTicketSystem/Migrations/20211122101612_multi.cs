using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class multi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Categories_CategoryID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CategoryID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "SupportType",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a8b3522d-531f-4e36-a1ae-5c25b6412616");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "e1808bf9-74aa-481e-8e45-46bd4ddfcccd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "2d540b33-ede4-4e2c-9737-88dccf1352a4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7cad864b-3215-4370-ab40-7c28ec08dad1", "AQAAAAEAACcQAAAAECFDid/y2mTAajhZrLg7GLig1kTBRmGyC+BaLtfa1gi+v2SpqEt40DJM8YgIj3LBCQ==", "6fb000fa-8f74-4ce0-94ca-82d0f5a49568" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportType",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Tickets",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CategoryID",
                table: "Tickets",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Categories_CategoryID",
                table: "Tickets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
