using Microsoft.EntityFrameworkCore.Migrations;

namespace Equipment.Database.Migrations
{
    public partial class Owners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "EquipmentsKits");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "EquipmentsKits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chief = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentsKits_OwnerId",
                table: "EquipmentsKits",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentsKits_Owners_OwnerId",
                table: "EquipmentsKits",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentsKits_Owners_OwnerId",
                table: "EquipmentsKits");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentsKits_OwnerId",
                table: "EquipmentsKits");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "EquipmentsKits");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "EquipmentsKits",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
