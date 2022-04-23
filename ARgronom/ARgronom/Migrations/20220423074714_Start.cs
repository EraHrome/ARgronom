using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARgronom.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoilType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fertilizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FertilizerFrequency = table.Column<int>(type: "int", nullable: false),
                    MinToNextPlant = table.Column<int>(type: "int", nullable: false),
                    MinToNextGardenBed = table.Column<int>(type: "int", nullable: false),
                    NeedToTie = table.Column<bool>(type: "bit", nullable: false),
                    WateringFrequency = table.Column<int>(type: "int", nullable: false),
                    StandardGrowthTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchPlantsModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FertilizerFrequency = table.Column<int>(type: "int", nullable: false),
                    MinToNextPlant = table.Column<int>(type: "int", nullable: false),
                    MinToNextGardenBed = table.Column<int>(type: "int", nullable: false),
                    NeedToTie = table.Column<bool>(type: "bit", nullable: false),
                    WateringFrequency = table.Column<int>(type: "int", nullable: false),
                    StandardGrowthTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchPlantsModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViewObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewObjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SearchPlantsModels");

            migrationBuilder.DropTable(
                name: "ViewObjects");
        }
    }
}
