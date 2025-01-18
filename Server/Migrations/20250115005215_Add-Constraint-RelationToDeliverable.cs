using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintRelationToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Constrainsts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Constrainsts_DeliverableId",
                table: "Constrainsts",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constrainsts_Deliverables_DeliverableId",
                table: "Constrainsts",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constrainsts_Deliverables_DeliverableId",
                table: "Constrainsts");

            migrationBuilder.DropIndex(
                name: "IX_Constrainsts_DeliverableId",
                table: "Constrainsts");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Constrainsts");
        }
    }
}
