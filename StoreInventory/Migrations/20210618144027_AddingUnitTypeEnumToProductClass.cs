using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreInventory.Migrations
{
    public partial class AddingUnitTypeEnumToProductClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitType",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Products");
        }
    }
}
