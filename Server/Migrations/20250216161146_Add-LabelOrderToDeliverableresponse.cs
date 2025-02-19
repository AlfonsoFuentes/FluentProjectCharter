using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLabelOrderToDeliverableresponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNodeOpened",
                table: "Deliverables");

            migrationBuilder.RenameColumn(
                name: "sOrder",
                table: "Deliverables",
                newName: "WBS");

            migrationBuilder.AddColumn<int>(
                name: "LabelOrder",
                table: "Deliverables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelOrder",
                table: "Deliverables");

            migrationBuilder.RenameColumn(
                name: "WBS",
                table: "Deliverables",
                newName: "sOrder");

            migrationBuilder.AddColumn<bool>(
                name: "IsNodeOpened",
                table: "Deliverables",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
