using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseEquipments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Testings");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Structurals");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Paintings");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Engineerings");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Electricals");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "EHSs");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Alterations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Valves",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Testings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Taxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Structurals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Paintings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Isometrics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Instruments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Foundations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Engineerings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Electricals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "EHSs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Alterations",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
