using Microsoft.EntityFrameworkCore.Migrations;

namespace YHVegeterianFoodOrderingSystem.Migrations.YHVegeterianFoodOrderingSystemContextNewMigrations
{
    public partial class FoodImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodImagePath",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodImagePath",
                table: "Menu");
        }
    }
}
