using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLagToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTime",
                table: "Deliverables");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lag",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "Lag",
                table: "Deliverables");

            migrationBuilder.AddColumn<string>(
                name: "DurationTime",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
