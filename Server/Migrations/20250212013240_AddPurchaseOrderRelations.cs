using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseOrderRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountAssigment",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrencyDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "PurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCapitalizedSalary",
                table: "PurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxEditable",
                table: "PurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PONumber",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderCurrency",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderStatus",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseRequisition",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseorderName",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuoteCurrency",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuoteNo",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SPL",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxCode",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "USDCOP",
                table: "PurchaseOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "USDEUR",
                table: "PurchaseOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxAlteration",
                table: "PurchaseOrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxNoProductive",
                table: "PurchaseOrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PurchaseOrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "PurchaseOrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UnitaryValueCurrency",
                table: "PurchaseOrderItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItemReceiveds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueReceivedCurrency = table.Column<double>(type: "float", nullable: false),
                    USDCOP = table.Column<double>(type: "float", nullable: false),
                    USDEUR = table.Column<double>(type: "float", nullable: false),
                    CurrencyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_PurchaseOrderItemReceiveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItemReceiveds_PurchaseOrderItems_PurchaseOrderItemId",
                        column: x => x.PurchaseOrderItemId,
                        principalTable: "PurchaseOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItemReceiveds_PurchaseOrderItemId",
                table: "PurchaseOrderItemReceiveds",
                column: "PurchaseOrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderItemReceiveds");

            migrationBuilder.DropColumn(
                name: "AccountAssigment",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CurrencyDate",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ExpectedDate",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IsCapitalizedSalary",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IsTaxEditable",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PONumber",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderCurrency",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderStatus",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseRequisition",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseorderName",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "QuoteCurrency",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "QuoteNo",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SPL",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "USDCOP",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "USDEUR",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IsTaxAlteration",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "IsTaxNoProductive",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "UnitaryValueCurrency",
                table: "PurchaseOrderItems");
        }
    }
}
