using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Addcompleteproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contingencys");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Engineerings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "Engineerings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Contingencys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliverableId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contingencys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contingencys_Deliverables_DeliverableId",
                        column: x => x.DeliverableId,
                        principalTable: "Deliverables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contingencys_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contingencys_DeliverableId",
                table: "Contingencys",
                column: "DeliverableId");

            migrationBuilder.CreateIndex(
                name: "IX_Contingencys_ProjectId",
                table: "Contingencys",
                column: "ProjectId");
        }
    }
}
