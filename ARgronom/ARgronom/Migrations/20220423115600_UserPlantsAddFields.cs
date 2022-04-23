using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARgronom.Migrations
{
    public partial class UserPlantsAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastWateringTime",
                table: "UserPlants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RecentFertilizer",
                table: "UserPlants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastWateringTime",
                table: "UserPlants");

            migrationBuilder.DropColumn(
                name: "RecentFertilizer",
                table: "UserPlants");
        }
    }
}
