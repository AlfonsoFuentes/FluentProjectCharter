using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipmentInstrumentValves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlowDiagram_Projects_ProjectId",
                table: "ProcessFlowDiagram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessFlowDiagram",
                table: "ProcessFlowDiagram");

            migrationBuilder.RenameTable(
                name: "ProcessFlowDiagram",
                newName: "ProcessFlowDiagrams");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessFlowDiagram_ProjectId",
                table: "ProcessFlowDiagrams",
                newName: "IX_ProcessFlowDiagrams_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessFlowDiagrams",
                table: "ProcessFlowDiagrams",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nozzles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemConnectedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EndType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominalDiameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OuterDiameter = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: false),
                    OuterDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThicknessUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeightDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nozzles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NozzleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NozzleTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariableInstrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierInstrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValveTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActuadorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasFeedBack = table.Column<bool>(type: "bit", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValveTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValveTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTemplates_EquipmentTemplateId",
                        column: x => x.EquipmentTemplateId,
                        principalTable: "EquipmentTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Equipments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_InstrumentTemplates_InstrumentTemplateId",
                        column: x => x.InstrumentTemplateId,
                        principalTable: "InstrumentTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instruments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValveTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valves_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valves_ValveTemplates_ValveTemplateId",
                        column: x => x.ValveTemplateId,
                        principalTable: "ValveTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTemplateId",
                table: "Equipments",
                column: "EquipmentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ProjectId",
                table: "Equipments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTemplates_BrandTemplateId",
                table: "EquipmentTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_InstrumentTemplateId",
                table: "Instruments",
                column: "InstrumentTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_ProjectId",
                table: "Instruments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTemplates_BrandTemplateId",
                table: "InstrumentTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Nozzles_EngineeringItemId",
                table: "Nozzles",
                column: "EngineeringItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Nozzles_ItemConnectedId",
                table: "Nozzles",
                column: "ItemConnectedId");

            migrationBuilder.CreateIndex(
                name: "IX_NozzleTemplates_TemplateId",
                table: "NozzleTemplates",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ProjectId",
                table: "Valves",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Valves_ValveTemplateId",
                table: "Valves",
                column: "ValveTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ValveTemplates_BrandTemplateId",
                table: "ValveTemplates",
                column: "BrandTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlowDiagrams_Projects_ProjectId",
                table: "ProcessFlowDiagrams",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlowDiagrams_Projects_ProjectId",
                table: "ProcessFlowDiagrams");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Nozzles");

            migrationBuilder.DropTable(
                name: "NozzleTemplates");

            migrationBuilder.DropTable(
                name: "Valves");

            migrationBuilder.DropTable(
                name: "EquipmentTemplates");

            migrationBuilder.DropTable(
                name: "InstrumentTemplates");

            migrationBuilder.DropTable(
                name: "ValveTemplates");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessFlowDiagrams",
                table: "ProcessFlowDiagrams");

            migrationBuilder.RenameTable(
                name: "ProcessFlowDiagrams",
                newName: "ProcessFlowDiagram");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessFlowDiagrams_ProjectId",
                table: "ProcessFlowDiagram",
                newName: "IX_ProcessFlowDiagram_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessFlowDiagram",
                table: "ProcessFlowDiagram",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlowDiagram_Projects_ProjectId",
                table: "ProcessFlowDiagram",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
