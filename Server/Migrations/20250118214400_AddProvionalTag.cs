using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddProvionalTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProvisionalTag",
                table: "Valves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProvisionalTag",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProvisionalTag",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProvisionalTag",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvisionalTag",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "ProvisionalTag",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "ProvisionalTag",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "ProvisionalTag",
                table: "Equipments");
        }
    }
}
