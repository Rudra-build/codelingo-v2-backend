using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLingo.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFreeQuizLimitField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastFreeQuizGeneratedDate",
                table: "Users",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFreeQuizGeneratedDate",
                table: "Users");
        }
    }
}
