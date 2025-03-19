using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSponsorManagerFromProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_StakeHolders_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_StakeHolders_SponsorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_SponsorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SponsorId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SponsorId",
                table: "Projects",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_StakeHolders_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "StakeHolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_StakeHolders_SponsorId",
                table: "Projects",
                column: "SponsorId",
                principalTable: "StakeHolders",
                principalColumn: "Id");
        }
    }
}
