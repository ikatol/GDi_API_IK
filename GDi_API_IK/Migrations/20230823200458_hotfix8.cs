using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDi_API_IK.Migrations
{
    /// <inheritdoc />
    public partial class hotfix8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDrivers_Cars_CarId",
                table: "CarDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarDrivers_Drivers_DriverId",
                table: "CarDrivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarDrivers",
                table: "CarDrivers");

            migrationBuilder.RenameTable(
                name: "CarDrivers",
                newName: "CarDriver");

            migrationBuilder.RenameIndex(
                name: "IX_CarDrivers_DriverId",
                table: "CarDriver",
                newName: "IX_CarDriver_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_CarDrivers_CarId",
                table: "CarDriver",
                newName: "IX_CarDriver_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarDriver",
                table: "CarDriver",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDriver_Cars_CarId",
                table: "CarDriver",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarDriver_Drivers_DriverId",
                table: "CarDriver",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDriver_Cars_CarId",
                table: "CarDriver");

            migrationBuilder.DropForeignKey(
                name: "FK_CarDriver_Drivers_DriverId",
                table: "CarDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarDriver",
                table: "CarDriver");

            migrationBuilder.RenameTable(
                name: "CarDriver",
                newName: "CarDrivers");

            migrationBuilder.RenameIndex(
                name: "IX_CarDriver_DriverId",
                table: "CarDrivers",
                newName: "IX_CarDrivers_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_CarDriver_CarId",
                table: "CarDrivers",
                newName: "IX_CarDrivers_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarDrivers",
                table: "CarDrivers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDrivers_Cars_CarId",
                table: "CarDrivers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarDrivers_Drivers_DriverId",
                table: "CarDrivers",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
