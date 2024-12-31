using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssueLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StakeHolderIssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StakeHolderResponsibleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SolveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HowToSolveIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueLogs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLogs_StakeHolders_StakeHolderIssuerId",
                        column: x => x.StakeHolderIssuerId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueLogs_StakeHolders_StakeHolderResponsibleId",
                        column: x => x.StakeHolderResponsibleId,
                        principalTable: "StakeHolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_ProjectId",
                table: "IssueLogs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_StakeHolderIssuerId",
                table: "IssueLogs",
                column: "StakeHolderIssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLogs_StakeHolderResponsibleId",
                table: "IssueLogs",
                column: "StakeHolderResponsibleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLogs");
        }
    }
}
