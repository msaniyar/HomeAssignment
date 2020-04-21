using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAssignmentAPI.Migrations
{
    public partial class CreateHomeAssignmentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentList",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    EquipmentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentList", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RentedHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RentedDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentedHistory_EquipmentList_Name",
                        column: x => x.Name,
                        principalTable: "EquipmentList",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EquipmentList",
                columns: new[] { "Name", "EquipmentType" },
                values: new object[,]
                {
                    { "Caterpillar bulldozer", "Heavy" },
                    { "KamAZ truck", "Regular" },
                    { "Komatsu crane", "Heavy" },
                    { "Volvo steamroller", "Regular" },
                    { "Bosch jackhammer", "Specialized" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentedHistory_Name",
                table: "RentedHistory",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentedHistory");

            migrationBuilder.DropTable(
                name: "EquipmentList");
        }
    }
}
