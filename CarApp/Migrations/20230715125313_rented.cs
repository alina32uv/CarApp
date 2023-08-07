using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class rented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentInfo_Insurance_InsuranceId",
                table: "RentInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentInfo_InsuranceId",
                table: "RentInfo");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "RentInfo");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleTypeId",
                table: "CarViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarBodyTypeId",
                table: "CarViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Car",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Car");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "VehicleType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "RentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleTypeId",
                table: "CarViewModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarBodyTypeId",
                table: "CarViewModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RentInfo_InsuranceId",
                table: "RentInfo",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfo_Insurance_InsuranceId",
                table: "RentInfo",
                column: "InsuranceId",
                principalTable: "Insurance",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
