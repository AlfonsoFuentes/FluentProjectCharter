using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReOrderBudegtItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Valves",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Testings",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Taxes",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Structurals",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Paintings",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Isometrics",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Instruments",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Foundations",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Equipments",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "EngineeringSalarys",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Engineerings",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Electricals",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "EHSs",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Contingencys",
                newName: "BudgetUSD");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "Alterations",
                newName: "BudgetUSD");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Valves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Valves",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Testings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Testings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Taxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Taxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Structurals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Structurals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Paintings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Paintings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Isometrics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Isometrics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Instruments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Instruments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Foundations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Foundations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Equipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Equipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "EngineeringSalarys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "EngineeringSalarys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Engineerings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Engineerings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Electricals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Electricals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "EHSs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "EHSs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Contingencys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Contingencys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlteration",
                table: "Alterations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxes",
                table: "Alterations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Valves");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Testings");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Testings");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Structurals");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Structurals");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Paintings");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Paintings");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Isometrics");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Foundations");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "EngineeringSalarys");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "EngineeringSalarys");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Engineerings");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Engineerings");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Electricals");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Electricals");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "EHSs");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "EHSs");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Contingencys");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Contingencys");

            migrationBuilder.DropColumn(
                name: "IsAlteration",
                table: "Alterations");

            migrationBuilder.DropColumn(
                name: "IsTaxes",
                table: "Alterations");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Valves",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Testings",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Taxes",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Structurals",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Paintings",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Isometrics",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Instruments",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Foundations",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Equipments",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "EngineeringSalarys",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Engineerings",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Electricals",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "EHSs",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Contingencys",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "BudgetUSD",
                table: "Alterations",
                newName: "Budget");
        }
    }
}
