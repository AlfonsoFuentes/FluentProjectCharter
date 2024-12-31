using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEngineeringItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcessFlowDiagramId",
                table: "Valves",
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

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ProcessFlowDiagramId",
                table: "Valves",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ProcessFlowDiagramId",
                table: "Instruments",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ProcessFlowDiagramId",
                table: "Equipments",
                column: "ProcessFlowDiagramId");

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
                name: "FK_Valves_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Valves",
                column: "ProcessFlowDiagramId",
                principalTable: "ProcessFlowDiagrams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_Valves_ProcessFlowDiagrams_ProcessFlowDiagramId",
                table: "Valves");

            migrationBuilder.DropIndex(
                name: "IX_Valves_ProcessFlowDiagramId",
                table: "Valves");

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
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "ProcessFlowDiagramId",
                table: "Equipments");
        }
    }
}
