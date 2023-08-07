using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class statusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "RentInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "RentInfo");
        }
    }
}
