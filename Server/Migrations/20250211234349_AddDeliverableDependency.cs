using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliverableDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DependencyType",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DependentantId",
                table: "Deliverables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DurationTime",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Deliverables",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentDeliverableId",
                table: "Deliverables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Deliverables",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverables_DependentantId",
                table: "Deliverables",
                column: "DependentantId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverables_ParentDeliverableId",
                table: "Deliverables",
                column: "ParentDeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_Deliverables_DependentantId",
                table: "Deliverables",
                column: "DependentantId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_Deliverables_ParentDeliverableId",
                table: "Deliverables",
                column: "ParentDeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_Deliverables_DependentantId",
                table: "Deliverables");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_Deliverables_ParentDeliverableId",
                table: "Deliverables");

            migrationBuilder.DropIndex(
                name: "IX_Deliverables_DependentantId",
                table: "Deliverables");

            migrationBuilder.DropIndex(
                name: "IX_Deliverables_ParentDeliverableId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "DependencyType",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "DependentantId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "DurationTime",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "ParentDeliverableId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Deliverables");
        }
    }
}
