using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIsexisting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExisting",
                table: "Valves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExisting",
                table: "Isometrics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExisting",
                table: "Instruments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExisting",
                table: "Equipments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExisting",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "IsExisting",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "IsExisting",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "IsExisting",
                table: "Equipments");
        }
    }
}
