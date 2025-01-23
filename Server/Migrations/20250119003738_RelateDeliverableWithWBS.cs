using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RelateDeliverableWithWBS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "WBSComponents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WBSComponents_DeliverableId",
                table: "WBSComponents",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_WBSComponents_Deliverables_DeliverableId",
                table: "WBSComponents",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WBSComponents_Deliverables_DeliverableId",
                table: "WBSComponents");

            migrationBuilder.DropIndex(
                name: "IX_WBSComponents_DeliverableId",
                table: "WBSComponents");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "WBSComponents");
        }
    }
}
