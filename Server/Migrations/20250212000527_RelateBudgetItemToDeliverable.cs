using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RelateBudgetItemToDeliverable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Valves",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Testings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Taxes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Structurals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Paintings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Isometrics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Instruments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Foundations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Engineerings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Electricals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "EHSs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliverableId",
                table: "Alterations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BudgetItemValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetItemValues_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Valves_DeliverableId",
                table: "Valves",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Testings_DeliverableId",
                table: "Testings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_DeliverableId",
                table: "Taxes",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Structurals_DeliverableId",
                table: "Structurals",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Paintings_DeliverableId",
                table: "Paintings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_DeliverableId",
                table: "Isometrics",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_DeliverableId",
                table: "Instruments",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Foundations_DeliverableId",
                table: "Foundations",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_DeliverableId",
                table: "Equipments",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Engineerings_DeliverableId",
                table: "Engineerings",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Electricals_DeliverableId",
                table: "Electricals",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_EHSs_DeliverableId",
                table: "EHSs",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Alterations_DeliverableId",
                table: "Alterations",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItemValues_DeliverableId",
                table: "BudgetItemValues",
                column: "DeliverableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alterations_Deliverables_DeliverableId",
                table: "Alterations",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EHSs_Deliverables_DeliverableId",
                table: "EHSs",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Electricals_Deliverables_DeliverableId",
                table: "Electricals",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineerings_Deliverables_DeliverableId",
                table: "Engineerings",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Deliverables_DeliverableId",
                table: "Equipments",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foundations_Deliverables_DeliverableId",
                table: "Foundations",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_Deliverables_DeliverableId",
                table: "Instruments",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Isometrics_Deliverables_DeliverableId",
                table: "Isometrics",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paintings_Deliverables_DeliverableId",
                table: "Paintings",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Structurals_Deliverables_DeliverableId",
                table: "Structurals",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Deliverables_DeliverableId",
                table: "Taxes",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Testings_Deliverables_DeliverableId",
                table: "Testings",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Valves_Deliverables_DeliverableId",
                table: "Valves",
                column: "DeliverableId",
                principalTable: "Deliverables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alterations_Deliverables_DeliverableId",
                table: "Alterations");

            migrationBuilder.DropForeignKey(
                name: "FK_EHSs_Deliverables_DeliverableId",
                table: "EHSs");

            migrationBuilder.DropForeignKey(
                name: "FK_Electricals_Deliverables_DeliverableId",
                table: "Electricals");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineerings_Deliverables_DeliverableId",
                table: "Engineerings");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Deliverables_DeliverableId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Foundations_Deliverables_DeliverableId",
                table: "Foundations");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_Deliverables_DeliverableId",
                table: "Instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_Isometrics_Deliverables_DeliverableId",
                table: "Isometrics");

            migrationBuilder.DropForeignKey(
                name: "FK_Paintings_Deliverables_DeliverableId",
                table: "Paintings");

            migrationBuilder.DropForeignKey(
                name: "FK_Structurals_Deliverables_DeliverableId",
                table: "Structurals");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxes_Deliverables_DeliverableId",
                table: "Taxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Testings_Deliverables_DeliverableId",
                table: "Testings");

            migrationBuilder.DropForeignKey(
                name: "FK_Valves_Deliverables_DeliverableId",
                table: "Valves");

            migrationBuilder.DropTable(
                name: "BudgetItemValues");

            migrationBuilder.DropIndex(
                name: "IX_Valves_DeliverableId",
                table: "Valves");

            migrationBuilder.DropIndex(
                name: "IX_Testings_DeliverableId",
                table: "Testings");

            migrationBuilder.DropIndex(
                name: "IX_Taxes_DeliverableId",
                table: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_Structurals_DeliverableId",
                table: "Structurals");

            migrationBuilder.DropIndex(
                name: "IX_Paintings_DeliverableId",
                table: "Paintings");

            migrationBuilder.DropIndex(
                name: "IX_Isometrics_DeliverableId",
                table: "Isometrics");

            migrationBuilder.DropIndex(
                name: "IX_Instruments_DeliverableId",
                table: "Instruments");

            migrationBuilder.DropIndex(
                name: "IX_Foundations_DeliverableId",
                table: "Foundations");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_DeliverableId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Engineerings_DeliverableId",
                table: "Engineerings");

            migrationBuilder.DropIndex(
                name: "IX_Electricals_DeliverableId",
                table: "Electricals");

            migrationBuilder.DropIndex(
                name: "IX_EHSs_DeliverableId",
                table: "EHSs");

            migrationBuilder.DropIndex(
                name: "IX_Alterations_DeliverableId",
                table: "Alterations");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Testings");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Structurals");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Paintings");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Engineerings");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Electricals");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "EHSs");

            migrationBuilder.DropColumn(
                name: "DeliverableId",
                table: "Alterations");
        }
    }
}
