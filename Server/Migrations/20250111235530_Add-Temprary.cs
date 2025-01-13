using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTemprary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temporarys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EquipmentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValveTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstrumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InstrumentTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PipingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PipingTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TagLetter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Temporarys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temporarys");
        }
    }
}
