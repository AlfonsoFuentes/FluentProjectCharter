using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSomeDataDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assumptions_Deliverables_DeliverableId",
                table: "Assumptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Constrainsts_Deliverables_DeliverableId",
                table: "Constrainsts");

            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Deliverables_DeliverableId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_DeliverableId",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Constrainsts_DeliverableId",
                table: "Constrainsts");

            migrationBuilder.DropIndex(
                name: "IX_Assumptions_DeliverableId",
                table: "Assumptions");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Constrainsts");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Assumptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Constrainsts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Assumptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_DeliverableId",
                table: "Requirements",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Constrainsts_DeliverableId",
                table: "Constrainsts",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Assumptions_DeliverableId",
                table: "Assumptions",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assumptions_Deliverables_DeliverableId",
                table: "Assumptions",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Constrainsts_Deliverables_DeliverableId",
                table: "Constrainsts",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Deliverables_DeliverableId",
                table: "Requirements",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
