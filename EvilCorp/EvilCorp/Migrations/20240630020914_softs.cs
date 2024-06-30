using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EvilCorp.Migrations
{
    /// <inheritdoc />
    public partial class softs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "IdSoftware", "Category", "Name", "Price", "SoftInfo", "VerInfo", "Version" },
                values: new object[,]
                {
                    { 1, "Digital image processing software", "Lightroom", 5899.99m, "Image editing software", "Not the latest version", "2023.9.9" },
                    { 2, "Digital image processing software", "Lightroom", 5950.00m, "Image editing software", "Not the latest version", "2024.0.0" },
                    { 3, "Digital image processing software", "Lightroom", 5999.99m, "Image editing software", "Latest version", "2024.1.0" },
                    { 4, "Graphic design software", "Photoshop", 5599.99m, "Graphic design software", "Not the latest version", "2023.8.0" },
                    { 5, "Graphic design software", "Photoshop", 5699.99m, "Graphic design software", "Not the latest version", "2023.9.0" },
                    { 6, "Graphic design software", "Photoshop", 5799.99m, "Graphic design software", "Not the latest version", "2023.9.9" },
                    { 7, "Graphic design software", "Photoshop", 5899.99m, "Graphic design software", "Not the latest version", "2024.0.0" },
                    { 8, "Graphic design software", "Photoshop", 6099.99m, "Graphic design software", "Latest version", "2024.1.1" },
                    { 9, "Simple Graphic design software", "Paint 3D PRO", 99.99m, "Graphic design software", "Latest version", "2022.7.4" },
                    { 10, "Graphic design software", "Illustrator", 3999.99m, "Graphic design software", "Not the latest version", "2024.3.9" },
                    { 11, "Simple Graphic design software", "Illustrator", 3999.99m, "Graphic design software", "Latest version", "2024.4.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Software",
                keyColumn: "IdSoftware",
                keyValue: 11);
        }
    }
}
