using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseAcordionActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "CaseTabActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "DeliverableAcordionActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "DeliverableTabActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "ProjectAcordionActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "ProjectTabActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "ScopeAcordionActive",
                table: "AppStates");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "AppStates",
                newName: "ActiveProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActiveProjectId",
                table: "AppStates",
                newName: "ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "CaseAcordionActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseTabActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliverableAcordionActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliverableTabActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectAcordionActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTabActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScopeAcordionActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
