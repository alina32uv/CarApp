using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class nouCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentInfo_Customer_CustomerId",
                table: "RentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_RentInfo_Status_StatusId",
                table: "RentInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentInfo_StatusId",
                table: "RentInfo");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "RentInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RentInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RentInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfo_Customer_CustomerId",
                table: "RentInfo",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentInfo_Customer_CustomerId",
                table: "RentInfo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RentInfo");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "RentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentInfo_StatusId",
                table: "RentInfo",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfo_Customer_CustomerId",
                table: "RentInfo",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfo_Status_StatusId",
                table: "RentInfo",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
