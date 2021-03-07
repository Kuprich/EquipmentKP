using Microsoft.EntityFrameworkCore.Migrations;

namespace Equipment.Database.Migrations
{
    public partial class num_to_no : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "MainEquipment",
                newName: "SerialNo");

            migrationBuilder.RenameColumn(
                name: "InventoryNum",
                table: "EquipmentsKits",
                newName: "InventoryNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerialNo",
                table: "MainEquipment",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "InventoryNo",
                table: "EquipmentsKits",
                newName: "InventoryNum");
        }
    }
}
