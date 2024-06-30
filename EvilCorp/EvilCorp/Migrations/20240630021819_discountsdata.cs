using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EvilCorp.Migrations
{
    /// <inheritdoc />
    public partial class discountsdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "IdDiscount", "EndDate", "Info", "Name", "StartDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "March discounts", "Black March", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3m },
                    { 2, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "June discounts", "June Madness", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.5m },
                    { 3, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "For all new customers", "June for noobies", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5m },
                    { 4, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "July discounts", "July Madness", new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.9m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 4);
        }
    }
}
