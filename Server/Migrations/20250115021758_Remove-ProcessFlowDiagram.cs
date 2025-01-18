using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProcessFlowDiagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_Isometrics_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Isometrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Valves_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Valves");

            migrationBuilder.DropTable(
                name: "ProcessFlowDiagrams");

            migrationBuilder.DropIndex(
                name: "IX_Valves_ProcessFlowDiagramId",
                table: "Valves");

            migrationBuilder.DropIndex(
                name: "IX_Isometrics_ProcessFlowDiagramId",
                table: "Isometrics");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_ProcessFlowDiagramId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_ProcessFlowDiagramId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ProcessFlowDiagramId",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "ProcessFlowDiagramId",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "ProcessFlowDiagramId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "ProcessFlowDiagramId",
                table: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessFlowDiagramId",
                table: "Valves",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessFlowDiagramId",
                table: "Isometrics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessFlowDiagramId",
                table: "Instruments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessFlowDiagramId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProcessFlowDiagrams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessFlowDiagrams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessFlowDiagrams_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessFlowDiagrams_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ProcessFlowDiagramId",
                table: "Valves",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_ProcessFlowDiagramId",
                table: "Isometrics",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ProcessFlowDiagramId",
                table: "Instruments",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ProcessFlowDiagramId",
                table: "Equipments",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowDiagrams_DeliverableId",
                table: "ProcessFlowDiagrams",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessFlowDiagrams_ProjectId",
                table: "ProcessFlowDiagrams",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Equipments",
                column: "ProcessFlowDiagramId",
                principalTable: "ProcessFlowDiagrams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Instruments",
                column: "ProcessFlowDiagramId",
                principalTable: "ProcessFlowDiagrams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Isometrics_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Isometrics",
                column: "ProcessFlowDiagramId",
                principalTable: "ProcessFlowDiagrams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Valves_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Valves",
                column: "ProcessFlowDiagramId",
                principalTable: "ProcessFlowDiagrams",
                principalColumn: "Id");
        }
    }
}
