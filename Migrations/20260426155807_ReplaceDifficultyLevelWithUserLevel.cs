using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLingo.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDifficultyLevelWithUserLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "DifficultyLevel",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
