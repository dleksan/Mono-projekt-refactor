using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monoProjekt.Migrations
{
    /// <inheritdoc />
    public partial class abrvbnbkhgnfhdgff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_vehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "vehicleMakeId",
                table: "VehicleModels",
                newName: "VehicleMakeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_vehicleMakeId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_VehicleMakeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleMakeId",
                table: "VehicleModels",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "MakeId",
                table: "VehicleModels",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels",
                column: "VehicleMakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_VehicleMakeId",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "VehicleMakeId",
                table: "VehicleModels",
                newName: "vehicleMakeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleModels_VehicleMakeId",
                table: "VehicleModels",
                newName: "IX_VehicleModels_vehicleMakeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "vehicleMakeId",
                table: "VehicleModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MakeId",
                table: "VehicleModels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_vehicleMakeId",
                table: "VehicleModels",
                column: "vehicleMakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
