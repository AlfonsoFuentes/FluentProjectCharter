using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseOrderRelationToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "PurchaseOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DeliverableId",
                table: "PurchaseOrders",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Deliverables_DeliverableId",
                table: "PurchaseOrders",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Deliverables_DeliverableId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_DeliverableId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "PurchaseOrders");
        }
    }
}
