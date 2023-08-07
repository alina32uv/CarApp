using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class StatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "RentModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentModel_StatusId",
                table: "RentModel",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentModel_Status_StatusId",
                table: "RentModel",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentModel_Status_StatusId",
                table: "RentModel");

            migrationBuilder.DropIndex(
                name: "IX_RentModel_StatusId",
                table: "RentModel");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "RentModel");
        }
    }
}
