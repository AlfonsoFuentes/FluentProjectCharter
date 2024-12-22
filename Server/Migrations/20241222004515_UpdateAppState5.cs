using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppState5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTab",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "ActiveScopeId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ActiveTab",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "ActiveTab",
                table: "Projects",
                newName: "ActiveScopeTab");

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

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveScopeId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCaseTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveDeliverableTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveProjectTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveScopeId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ActiveScopeTab",
                table: "Projects",
                newName: "ActiveTab");

            migrationBuilder.AddColumn<string>(
                name: "ActiveTab",
                table: "Deliverables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveScopeId",
                table: "Cases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActiveTab",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
