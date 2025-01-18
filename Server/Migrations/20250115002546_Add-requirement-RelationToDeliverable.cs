using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddrequirementRelationToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_DeliverableId",
                table: "Requirements",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Deliverables_DeliverableId",
                table: "Requirements",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Deliverables_DeliverableId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_DeliverableId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Requirements");
        }
    }
}
