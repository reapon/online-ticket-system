using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class supportmaillist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MailList",
                table: "UserMails",
                newName: "TicketMailList");

            migrationBuilder.AddColumn<string>(
                name: "SupportMailList",
                table: "UserMails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "08d7c215-0f4e-4eac-8855-6f96395d0334");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-421f-86af-483d87fd7210",
                column: "ConcurrencyStamp",
                value: "5bb60a4b-058e-485e-af8c-359973dac518");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "554c1354-67c9-4cba-a8d2-024a87abfc8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9550d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a99eff0-0ce4-490f-9581-353792b93dc2", "AQAAAAEAACcQAAAAEEhMGdwCRFJPpZMj0KIL+9z4gOyziKbT83gL6I4JT1zNnbXuxCihpQcajYhaTQbI4Q==", "126d8f85-6f3e-4691-bac8-a879c346edf4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportMailList",
                table: "UserMails");

            migrationBuilder.RenameColumn(
                name: "TicketMailList",
                table: "UserMails",
                newName: "MailList");

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
    }
}
