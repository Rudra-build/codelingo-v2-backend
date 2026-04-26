using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLingo.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStreakFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentStreak",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastQuizCompletedDate",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LongestStreak",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuizzesCompleted",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStreak",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastQuizCompletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LongestStreak",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalQuizzesCompleted",
                table: "Users");
        }
    }
}
