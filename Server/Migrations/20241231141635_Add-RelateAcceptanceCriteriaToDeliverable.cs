using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRelateAcceptanceCriteriaToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Requirements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Requirements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Requirements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AcceptanceCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptanceCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcceptanceCriterias_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_RequestedById",
                table: "Requirements",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ResponsibleId",
                table: "Requirements",
                column: "ResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AcceptanceCriterias_DeliverableId",
                table: "AcceptanceCriterias",
                column: "DeliverableId");

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

            migrationBuilder.DropTable(
                name: "AcceptanceCriterias");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_RequestedById",
                table: "Requirements");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_ResponsibleId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Requirements");
        }
    }
}
