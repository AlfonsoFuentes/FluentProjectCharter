using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliverableIsExpanded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Deliverables");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpanded",
                table: "Deliverables",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpanded",
                table: "Deliverables");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Deliverables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Deliverables",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
