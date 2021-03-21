using Microsoft.EntityFrameworkCore.Migrations;

namespace YHVegeterianFoodOrderingSystem.Migrations.YHVegeterianFoodOrderingSystemContextNewMigrations
{
    public partial class OrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodImagePath",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "UnitPrice",
                table: "PurchaseHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "PurchaseHistory");

            migrationBuilder.AddColumn<string>(
                name: "FoodImagePath",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
