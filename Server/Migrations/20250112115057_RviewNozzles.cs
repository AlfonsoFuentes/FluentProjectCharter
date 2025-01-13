using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RviewNozzles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipments");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "NozzleTemplates",
                newName: "NozzleType");

            migrationBuilder.RenameColumn(
                name: "Diameter",
                table: "NozzleTemplates",
                newName: "NominalDiameter");

            migrationBuilder.RenameColumn(
                name: "WeldType",
                table: "Nozzles",
                newName: "NozzleType");

            migrationBuilder.RenameColumn(
                name: "HeightDiameterUnit",
                table: "Nozzles",
                newName: "InnerDiameterUnit");

            migrationBuilder.RenameColumn(
                name: "EndType",
                table: "Nozzles",
                newName: "ConnectionType");

            migrationBuilder.AddColumn<double>(
                name: "InnerDiameter",
                table: "Nozzles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerDiameter",
                table: "Nozzles");

            migrationBuilder.RenameColumn(
                name: "NozzleType",
                table: "NozzleTemplates",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "NominalDiameter",
                table: "NozzleTemplates",
                newName: "Diameter");

            migrationBuilder.RenameColumn(
                name: "NozzleType",
                table: "Nozzles",
                newName: "WeldType");

            migrationBuilder.RenameColumn(
                name: "InnerDiameterUnit",
                table: "Nozzles",
                newName: "HeightDiameterUnit");

            migrationBuilder.RenameColumn(
                name: "ConnectionType",
                table: "Nozzles",
                newName: "EndType");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Valves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Isometrics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Instruments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
