using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppState3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectTabActive",
                table: "AppStates");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveCaseId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActiveTab",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ActiveProjectId",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCaseId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveTab",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ActiveTab",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "ActiveScopeId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "ActiveTab",
                table: "Cases");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActiveProjectId",
                table: "AppStates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTabActive",
                table: "AppStates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
