using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePipingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "FluidCodeName",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Insulation",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "LaborUnitaryCost",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "MaterialUnitaryCost",
                table: "Isometrics");

            migrationBuilder.AddColumn<bool>(
                name: "Insulation",
                table: "PipeTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PipeTemplateId",
                table: "Isometrics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Isometrics_PipeTemplates_FluidCodeId",
                table: "Isometrics",
                column: "FluidCodeId",
                principalTable: "PipeTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Isometrics_PipeTemplates_FluidCodeId",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "Insulation",
                table: "PipeTemplates");

            migrationBuilder.DropColumn(
                name: "PipeTemplateId",
                table: "Isometrics");

            migrationBuilder.AddColumn<string>(
                name: "Diameter",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FluidCodeName",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Insulation",
                table: "Isometrics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "LaborUnitaryCost",
                table: "Isometrics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Isometrics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MaterialUnitaryCost",
                table: "Isometrics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
