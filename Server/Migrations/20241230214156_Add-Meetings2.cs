using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "MeetingAttendantSuggestions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateofSuggestion",
                table: "MeetingAttendantSuggestions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofSuggestion",
                table: "MeetingAttendantSuggestions");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "MeetingAttendantSuggestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
