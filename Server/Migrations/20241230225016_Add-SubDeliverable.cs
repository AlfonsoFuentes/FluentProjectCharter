using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSubDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubDeliverableId",
                table: "Deliverables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliverables_SubDeliverableId",
                table: "Deliverables",
                column: "SubDeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_Deliverables_SubDeliverableId",
                table: "Deliverables",
                column: "SubDeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_Deliverables_SubDeliverableId",
                table: "Deliverables");

            migrationBuilder.DropIndex(
                name: "IX_Deliverables_SubDeliverableId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "SubDeliverableId",
                table: "Deliverables");
        }
    }
}
