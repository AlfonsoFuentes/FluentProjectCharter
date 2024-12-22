using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaseAcordionActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseTabActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliverableAcordionActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliverableTabActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectAcordionActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTabActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScopeAcordionActive",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseAcordionActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CaseTabActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeliverableAcordionActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeliverableTabActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectAcordionActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectTabActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ScopeAcordionActive",
                table: "Projects");
        }
    }
}
