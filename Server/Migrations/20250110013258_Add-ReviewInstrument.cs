using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewInstrument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "ExternalMaterial",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "InternalMaterial",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "ModifierInstrument",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "VariableInstrument",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Valves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalMaterial",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalMaterial",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifierInstrument",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VariableInstrument",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
