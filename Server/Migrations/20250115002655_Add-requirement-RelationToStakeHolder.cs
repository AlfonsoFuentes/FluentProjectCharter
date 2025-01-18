using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddrequirementRelationToStakeHolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestedById",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_RequestedById",
                table: "Requirements",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ResponsibleId",
                table: "Requirements",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_StakeHolders_RequestedById",
                table: "Requirements",
                column: "RequestedById",
                principalTable: "StakeHolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_StakeHolders_ResponsibleId",
                table: "Requirements",
                column: "ResponsibleId",
                principalTable: "StakeHolders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_StakeHolders_RequestedById",
                table: "Requirements");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_StakeHolders_ResponsibleId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_RequestedById",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_ResponsibleId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Requirements");
        }
    }
}
