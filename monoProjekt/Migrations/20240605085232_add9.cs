using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monoProjekt.Migrations
{
    /// <inheritdoc />
    public partial class add9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Abrv",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abrv",
                table: "Items");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DueAt",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
