using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppState6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCaseId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveCaseTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveDeliverableTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveProjectTab",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ActiveScopeTab",
                table: "Projects",
                newName: "ProjectTab");

            migrationBuilder.RenameColumn(
                name: "ActiveScopeId",
                table: "Projects",
                newName: "CaseId");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Scopes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScopeTab",
                table: "Scopes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliverableTab",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CaseTab",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ScopeId",
                table: "Cases",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Scopes");

            migrationBuilder.DropColumn(
                name: "ScopeTab",
                table: "Scopes");

            migrationBuilder.DropColumn(
                name: "DeliverableTab",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "CaseTab",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ScopeId",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "ProjectTab",
                table: "Projects",
                newName: "ActiveScopeTab");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "Projects",
                newName: "ActiveScopeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveCaseId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActiveCaseTab",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActiveDeliverableTab",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActiveProjectTab",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
