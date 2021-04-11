using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DataAccess.Migrations
{
    public partial class dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "id", "Name" },
                values: new object[] { 2, "system.admin" });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "id", "Name" },
                values: new object[] { 3, "customer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
