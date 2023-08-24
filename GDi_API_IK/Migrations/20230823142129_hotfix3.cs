using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDi_API_IK.Migrations
{
    /// <inheritdoc />
    public partial class hotfix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DriverID",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Cars");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CarId",
                table: "Drivers",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Cars_CarId",
                table: "Drivers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Cars_CarId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_CarId",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverID",
                table: "Cars",
                column: "DriverID",
                unique: true,
                filter: "[DriverID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
