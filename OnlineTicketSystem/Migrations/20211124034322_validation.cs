using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineTicketSystem.Migrations
{
    public partial class validation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MessageBody",
                table: "TicketMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MessageBody",
                table: "TicketMessages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
