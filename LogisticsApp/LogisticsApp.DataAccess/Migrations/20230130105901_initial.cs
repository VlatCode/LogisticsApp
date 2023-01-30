using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticsApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourierName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourierId = table.Column<int>(type: "int", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculations_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourierId = table.Column<int>(type: "int", nullable: false),
                    ValidationType = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Validations_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Dimensions = table.Column<int>(type: "int", nullable: false),
                    CalculationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Couriers",
                columns: new[] { "Id", "CourierName" },
                values: new object[,]
                {
                    { 1, "Cargo4You" },
                    { 2, "ShipFaster" },
                    { 3, "MaltaShip" }
                });

            migrationBuilder.InsertData(
                table: "Calculations",
                columns: new[] { "Id", "CalculationType", "Cost", "CourierId", "From", "To" },
                values: new object[,]
                {
                    { 1, 0, 15.0, 1, 0, 2 },
                    { 2, 0, 18.0, 1, 2, 15 },
                    { 3, 0, 35.0, 1, 15, 20 },
                    { 4, 1, 10.0, 1, 0, 1000 },
                    { 5, 1, 20.0, 1, 1000, 2000 },
                    { 6, 0, 16.5, 2, 10, 15 },
                    { 7, 0, 36.5, 2, 15, 25 },
                    { 8, 0, 40.0, 2, 25, 1000 },
                    { 9, 1, 11.99, 2, 0, 1000 },
                    { 10, 1, 21.989999999999998, 2, 1000, 1700 },
                    { 11, 0, 16.989999999999998, 3, 10, 20 },
                    { 12, 0, 33.990000000000002, 3, 20, 30 },
                    { 13, 0, 43.990000000000002, 3, 30, 1000 },
                    { 14, 1, 9.5, 3, 0, 1000 },
                    { 15, 1, 19.5, 3, 1000, 2000 },
                    { 16, 1, 48.5, 3, 2000, 5000 },
                    { 17, 1, 147.5, 3, 5000, 50000 }
                });

            migrationBuilder.InsertData(
                table: "Validations",
                columns: new[] { "Id", "CourierId", "From", "To", "ValidationType" },
                values: new object[,]
                {
                    { 1, 1, 0, 20, 0 },
                    { 2, 1, 0, 2000, 1 },
                    { 3, 2, 10, 30, 0 },
                    { 4, 2, 0, 1700, 1 },
                    { 5, 3, 0, 10, 0 },
                    { 6, 3, 500, 50000, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_CourierId",
                table: "Calculations",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CalculationId",
                table: "Packages",
                column: "CalculationId");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_CourierId",
                table: "Validations",
                column: "CourierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Validations");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "Couriers");
        }
    }
}
