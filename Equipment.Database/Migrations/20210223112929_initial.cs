using Microsoft.EntityFrameworkCore.Migrations;

namespace Equipment.Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainEquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainEquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubEquipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubEquipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainEquipmentTypeId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainEquipments_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainEquipments_MainEquipmentTypes_MainEquipmentTypeId",
                        column: x => x.MainEquipmentTypeId,
                        principalTable: "MainEquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubEquipmentTypeId = table.Column<int>(type: "int", nullable: true),
                    MainEquipmentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubEquipments_MainEquipments_MainEquipmentId",
                        column: x => x.MainEquipmentId,
                        principalTable: "MainEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubEquipments_SubEquipmentTypes_SubEquipmentTypeId",
                        column: x => x.SubEquipmentTypeId,
                        principalTable: "SubEquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainEquipments_LocationId",
                table: "MainEquipments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MainEquipments_MainEquipmentTypeId",
                table: "MainEquipments",
                column: "MainEquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubEquipments_MainEquipmentId",
                table: "SubEquipments",
                column: "MainEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubEquipments_SubEquipmentTypeId",
                table: "SubEquipments",
                column: "SubEquipmentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubEquipments");

            migrationBuilder.DropTable(
                name: "MainEquipments");

            migrationBuilder.DropTable(
                name: "SubEquipmentTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MainEquipmentTypes");
        }
    }
}
