using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeLingo.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PremiumStartedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PremiumStartedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "Users");
        }
    }
}
