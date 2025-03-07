using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlanning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "StakeHolders");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "StakeHolders");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Scopes");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Scopes");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Qualitys");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Qualitys");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "LearnedLessons");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "LearnedLessons");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "KnownRisks");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "KnownRisks");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "ExpertJudgements");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "ExpertJudgements");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Constrainsts");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Constrainsts");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Communications");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Communications");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Bennefits");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Bennefits");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Assumptions");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Assumptions");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "Acquisitions");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "Acquisitions");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "AcceptanceCriterias");

            migrationBuilder.DropColumn(
                name: "StartId",
                table: "AcceptanceCriterias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "StakeHolders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "StakeHolders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Scopes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Scopes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Resources",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Resources",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Requirements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Qualitys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Qualitys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Objectives",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Objectives",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "LearnedLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "LearnedLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "KnownRisks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "KnownRisks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "ExpertJudgements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "ExpertJudgements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Constrainsts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Constrainsts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Communications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Communications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Bennefits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Bennefits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Assumptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Assumptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "Acquisitions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "Acquisitions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "AcceptanceCriterias",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartId",
                table: "AcceptanceCriterias",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
