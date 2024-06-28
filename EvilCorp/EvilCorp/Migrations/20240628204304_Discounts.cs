using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvilCorp.Migrations
{
    /// <inheritdoc />
    public partial class Discounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Info = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.IdDiscount);
                });

            migrationBuilder.CreateTable(
                name: "AvailableDiscounts",
                columns: table => new
                {
                    DiscountsIdDiscount = table.Column<int>(type: "int", nullable: false),
                    SoftwaresIdSoftware = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDiscounts", x => new { x.DiscountsIdDiscount, x.SoftwaresIdSoftware });
                    table.ForeignKey(
                        name: "FK_AvailableDiscounts_Discount_DiscountsIdDiscount",
                        column: x => x.DiscountsIdDiscount,
                        principalTable: "Discount",
                        principalColumn: "IdDiscount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailableDiscounts_Software_SoftwaresIdSoftware",
                        column: x => x.SoftwaresIdSoftware,
                        principalTable: "Software",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDiscounts_SoftwaresIdSoftware",
                table: "AvailableDiscounts",
                column: "SoftwaresIdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableDiscounts");

            migrationBuilder.DropTable(
                name: "Discount");
        }
    }
}
