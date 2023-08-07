using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarApp.Migrations
{
    /// <inheritdoc />
    public partial class thisTransmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "TransmissionId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transmission",
                columns: table => new
                {
                    TransmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transmission", x => x.TransmissionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_TransmissionId",
                table: "Car",
                column: "TransmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Transmission_TransmissionId",
                table: "Car",
                column: "TransmissionId",
                principalTable: "Transmission",
                principalColumn: "TransmissionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Transmission_TransmissionId",
                table: "Car");

            migrationBuilder.DropTable(
                name: "Transmission");

            migrationBuilder.DropIndex(
                name: "IX_Car_TransmissionId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TransmissionId",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Car",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
