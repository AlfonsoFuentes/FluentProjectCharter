using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "ValveTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NozzleType",
                table: "ValveTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "PipeTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NozzleType",
                table: "PipeTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NozzleType",
                table: "InstrumentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "EquipmentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NozzleType",
                table: "EquipmentTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "ValveTemplates");

            migrationBuilder.DropColumn(
                name: "NozzleType",
                table: "ValveTemplates");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "PipeTemplates");

            migrationBuilder.DropColumn(
                name: "NozzleType",
                table: "PipeTemplates");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "NozzleType",
                table: "InstrumentTemplates");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "EquipmentTemplates");

            migrationBuilder.DropColumn(
                name: "NozzleType",
                table: "EquipmentTemplates");
        }
    }
}
