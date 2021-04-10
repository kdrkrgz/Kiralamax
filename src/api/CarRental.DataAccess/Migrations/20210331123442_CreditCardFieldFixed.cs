using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DataAccess.Migrations
{
    public partial class CreditCardFieldFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CardOwnerCustomerId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_CardOwnerCustomerId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "CardOwnerCustomerId",
                table: "CreditCards");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CreditCards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Customers_CustomerId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CreditCards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CardOwnerCustomerId",
                table: "CreditCards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CardOwnerCustomerId",
                table: "CreditCards",
                column: "CardOwnerCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Customers_CardOwnerCustomerId",
                table: "CreditCards",
                column: "CardOwnerCustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
