using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAppState2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ScopeAcordionActive",
                table: "AppStates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseTabActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectAcordionActive",
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
                    AppStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliverableAcordionActive = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliverableTabActive = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
