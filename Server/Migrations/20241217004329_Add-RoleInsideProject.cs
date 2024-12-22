using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleInsideProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleInsideProjectId",
                table: "StakeHolders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleInsideProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_RoleInsideProjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StakeHolders_RoleInsideProjectId",
                table: "StakeHolders",
                column: "RoleInsideProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StakeHolders_RoleInsideProjects_RoleInsideProjectId",
                table: "StakeHolders",
                column: "RoleInsideProjectId",
                principalTable: "RoleInsideProjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StakeHolders_RoleInsideProjects_RoleInsideProjectId",
                table: "StakeHolders");

            migrationBuilder.DropTable(
                name: "RoleInsideProjects");

            migrationBuilder.DropIndex(
                name: "IX_StakeHolders_RoleInsideProjectId",
                table: "StakeHolders");

            migrationBuilder.DropColumn(
                name: "RoleInsideProjectId",
                table: "StakeHolders");
        }
    }
}
