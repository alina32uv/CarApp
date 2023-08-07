using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class StatusAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RentInfo_StatusId",
                table: "RentInfo",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentInfo_Status_StatusId",
                table: "RentInfo",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentInfo_Status_StatusId",
                table: "RentInfo");

            migrationBuilder.DropIndex(
                name: "IX_RentInfo_StatusId",
                table: "RentInfo");
        }
    }
}
