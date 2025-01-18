using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReviewPipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Isometrics_PipeTemplates_FluidCodeId",
                table: "Isometrics");

            migrationBuilder.CreateIndex(
                name: "IX_Isometrics_PipeTemplateId",
                table: "Isometrics",
                column: "PipeTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Isometrics_PipeTemplates_PipeTemplateId",
                table: "Isometrics",
                column: "PipeTemplateId",
                principalTable: "PipeTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Isometrics_PipeTemplates_PipeTemplateId",
                table: "Isometrics");

            migrationBuilder.DropIndex(
                name: "IX_Isometrics_PipeTemplateId",
                table: "Isometrics");

            migrationBuilder.AddForeignKey(
                name: "FK_Isometrics_PipeTemplates_FluidCodeId",
                table: "Isometrics",
                column: "FluidCodeId",
                principalTable: "PipeTemplates",
                principalColumn: "Id");
        }
    }
}
