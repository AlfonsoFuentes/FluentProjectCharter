using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAppDeliverableState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveDeliverableState",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CaseAcordionActive",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CaseTabActive",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectAcordionActive",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTabActive",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScopeAcordionActive",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppStateDeliverables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableTabActive = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliverableAcordionActive = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStateDeliverables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStateDeliverables_AppStates_AppStateId",
                        column: x => x.AppStateId,
                        principalTable: "AppStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppStateDeliverables_AppStateId",
                table: "AppStateDeliverables",
                column: "AppStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppStateDeliverables");

            migrationBuilder.DropColumn(
                name: "ActiveDeliverableState",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "CaseAcordionActive",
                table: "AppStates");

            migrationBuilder.DropColumn(
                name: "CaseTabActive",
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
    }
}
