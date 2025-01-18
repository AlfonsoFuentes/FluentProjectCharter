using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReviewPiping2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diameter",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FluidCodeCode",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Insulation",
                table: "Isometrics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "FluidCodeCode",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Insulation",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Isometrics");
        }
    }
}
