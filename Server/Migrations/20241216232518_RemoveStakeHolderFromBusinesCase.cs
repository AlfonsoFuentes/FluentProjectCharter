using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStakeHolderFromBusinesCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StakeHolders_Cases_CaseId",
                table: "StakeHolders");

            migrationBuilder.DropIndex(
                name: "IX_StakeHolders_CaseId",
                table: "StakeHolders");

            migrationBuilder.DropColumn(
                name: "CaseId",
                table: "StakeHolders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CaseId",
                table: "StakeHolders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StakeHolders_CaseId",
                table: "StakeHolders",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StakeHolders_Cases_CaseId",
                table: "StakeHolders",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
