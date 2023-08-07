using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class carmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Car_CarId",
                table: "Brand");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Brand",
                newName: "CarViewModelCarId");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_CarId",
                table: "Brand",
                newName: "IX_Brand_CarViewModelCarId");

            migrationBuilder.AddColumn<int>(
                name: "CarViewModelCarId",
                table: "VehicleType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarViewModelCarId",
                table: "Transmission",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarViewModelCarId",
                table: "Fuel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarViewModelCarId",
                table: "Drive",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarViewModelCarId",
                table: "CarBodyType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarViewModel",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransmissionId = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    DriveId = table.Column<int>(type: "int", nullable: false),
                    DailyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarBodyTypeId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    FuelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarViewModel", x => x.CarId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleType_CarViewModelCarId",
                table: "VehicleType",
                column: "CarViewModelCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Transmission_CarViewModelCarId",
                table: "Transmission",
                column: "CarViewModelCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Fuel_CarViewModelCarId",
                table: "Fuel",
                column: "CarViewModelCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Drive_CarViewModelCarId",
                table: "Drive",
                column: "CarViewModelCarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyType_CarViewModelCarId",
                table: "CarBodyType",
                column: "CarViewModelCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_CarViewModel_CarViewModelCarId",
                table: "Brand",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarBodyType_CarViewModel_CarViewModelCarId",
                table: "CarBodyType",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drive_CarViewModel_CarViewModelCarId",
                table: "Drive",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fuel_CarViewModel_CarViewModelCarId",
                table: "Fuel",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transmission_CarViewModel_CarViewModelCarId",
                table: "Transmission",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleType_CarViewModel_CarViewModelCarId",
                table: "VehicleType",
                column: "CarViewModelCarId",
                principalTable: "CarViewModel",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_CarViewModel_CarViewModelCarId",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_CarBodyType_CarViewModel_CarViewModelCarId",
                table: "CarBodyType");

            migrationBuilder.DropForeignKey(
                name: "FK_Drive_CarViewModel_CarViewModelCarId",
                table: "Drive");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuel_CarViewModel_CarViewModelCarId",
                table: "Fuel");

            migrationBuilder.DropForeignKey(
                name: "FK_Transmission_CarViewModel_CarViewModelCarId",
                table: "Transmission");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleType_CarViewModel_CarViewModelCarId",
                table: "VehicleType");

            migrationBuilder.DropTable(
                name: "CarViewModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleType_CarViewModelCarId",
                table: "VehicleType");

            migrationBuilder.DropIndex(
                name: "IX_Transmission_CarViewModelCarId",
                table: "Transmission");

            migrationBuilder.DropIndex(
                name: "IX_Fuel_CarViewModelCarId",
                table: "Fuel");

            migrationBuilder.DropIndex(
                name: "IX_Drive_CarViewModelCarId",
                table: "Drive");

            migrationBuilder.DropIndex(
                name: "IX_CarBodyType_CarViewModelCarId",
                table: "CarBodyType");

            migrationBuilder.DropColumn(
                name: "CarViewModelCarId",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "CarViewModelCarId",
                table: "Transmission");

            migrationBuilder.DropColumn(
                name: "CarViewModelCarId",
                table: "Fuel");

            migrationBuilder.DropColumn(
                name: "CarViewModelCarId",
                table: "Drive");

            migrationBuilder.DropColumn(
                name: "CarViewModelCarId",
                table: "CarBodyType");

            migrationBuilder.RenameColumn(
                name: "CarViewModelCarId",
                table: "Brand",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_CarViewModelCarId",
                table: "Brand",
                newName: "IX_Brand_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Car_CarId",
                table: "Brand",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId");
        }
    }
}
