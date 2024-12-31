using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Addpiping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EngineeringFluidCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineeringFluidCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PipeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringItemType = table.Column<int>(type: "int", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquivalentLenghPrice = table.Column<double>(type: "float", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborDayPrice = table.Column<double>(type: "float", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipeTemplates_Brands_BrandTemplateId",
                        column: x => x.BrandTemplateId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesoryImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesoryImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Isometrics",
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
                    ProcessFlowDiagramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaterialUnitaryCost = table.Column<double>(type: "float", nullable: false),
                    MaterialQuantity = table.Column<double>(type: "float", nullable: false),
                    LaborUnitaryCost = table.Column<double>(type: "float", nullable: false),
                    LaborQuantity = table.Column<double>(type: "float", nullable: false),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FluidCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insulation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isometrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Isometrics_EngineeringFluidCodes_FluidCodeId",
                        column: x => x.FluidCodeId,
                        principalTable: "EngineeringFluidCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Isometrics_ProcessFlowDiagrams_ProcessFlowDiagramId",
                        column: x => x.ProcessFlowDiagramId,
                        principalTable: "ProcessFlowDiagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Isometrics_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipingCategorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingAccesoryImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingCategorys_PipingAccesoryImages_PipingAccesoryImageId",
                        column: x => x.PipingAccesoryImageId,
                        principalTable: "PipingAccesoryImages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesoryCodeBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PipingCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesoryCodeBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingAccesoryCodeBrands_PipingCategorys_PipingCategoryId",
                        column: x => x.PipingCategoryId,
                        principalTable: "PipingCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipingAccesorys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingAccesorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingAccesorys_PipingCategorys_PipingCategoryId",
                        column: x => x.PipingCategoryId,
                        principalTable: "PipingCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IsometricItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsometricId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PipingAccesoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsometricItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IsometricItems_Isometrics_IsometricId",
                        column: x => x.IsometricId,
                        principalTable: "Isometrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsometricItems_PipingAccesorys_PipingAccesoryId",
                        column: x => x.PipingAccesoryId,
                        principalTable: "PipingAccesorys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PipingConnectionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominalDiameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OuterDiameter = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: false),
                    OuterDiameterUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThicknessUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PipingAccesoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccesoryConnectionSide = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipingConnectionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipingConnectionTypes_PipingAccesorys_PipingAccesoryId",
                        column: x => x.PipingAccesoryId,
                        principalTable: "PipingAccesorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsometricItems_IsometricId",
                table: "IsometricItems",
                column: "IsometricId");

            migrationBuilder.CreateIndex(
                name: "IX_IsometricItems_PipingAccesoryId",
                table: "IsometricItems",
                column: "PipingAccesoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_FluidCodeId",
                table: "Isometrics",
                column: "FluidCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_ProcessFlowDiagramId",
                table: "Isometrics",
                column: "ProcessFlowDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_ProjectId",
                table: "Isometrics",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PipeTemplates_BrandTemplateId",
                table: "PipeTemplates",
                column: "BrandTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingAccesoryCodeBrands_PipingCategoryId",
                table: "PipingAccesoryCodeBrands",
                column: "PipingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingAccesorys_PipingCategoryId",
                table: "PipingAccesorys",
                column: "PipingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingCategorys_PipingAccesoryImageId",
                table: "PipingCategorys",
                column: "PipingAccesoryImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PipingConnectionTypes_PipingAccesoryId",
                table: "PipingConnectionTypes",
                column: "PipingAccesoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IsometricItems");

            migrationBuilder.DropTable(
                name: "PipeTemplates");

            migrationBuilder.DropTable(
                name: "PipingAccesoryCodeBrands");

            migrationBuilder.DropTable(
                name: "PipingConnectionTypes");

            migrationBuilder.DropTable(
                name: "Isometrics");

            migrationBuilder.DropTable(
                name: "PipingAccesorys");

            migrationBuilder.DropTable(
                name: "EngineeringFluidCodes");

            migrationBuilder.DropTable(
                name: "PipingCategorys");

            migrationBuilder.DropTable(
                name: "PipingAccesoryImages");
        }
    }
}
