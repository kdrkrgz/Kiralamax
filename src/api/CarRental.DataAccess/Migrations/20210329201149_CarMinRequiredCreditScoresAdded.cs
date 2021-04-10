using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DataAccess.Migrations
{
    public partial class CarMinRequiredCreditScoresAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinCreditScore",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinCreditScore",
                table: "Cars");
        }
    }
}
