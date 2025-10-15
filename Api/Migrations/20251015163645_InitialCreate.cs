using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Currency = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrencyProvider = table.Column<sbyte>(type: "tinyint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Mid = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Bid = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Ask = table.Column<decimal>(type: "decimal(18,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_Currency",
                table: "Rates",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_Currency_Date_CurrencyProvider",
                table: "Rates",
                columns: new[] { "Currency", "Date", "CurrencyProvider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CurrencyProvider",
                table: "Rates",
                column: "CurrencyProvider");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_Date",
                table: "Rates",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
