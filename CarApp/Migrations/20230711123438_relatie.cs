using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class relatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Brand",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CarId",
                table: "Brand",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Car_CarId",
                table: "Brand",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Car_CarId",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_CarId",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Brand");
        }
    }
}
