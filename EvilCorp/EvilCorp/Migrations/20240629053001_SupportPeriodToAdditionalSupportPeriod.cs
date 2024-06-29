using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvilCorp.Migrations
{
    /// <inheritdoc />
    public partial class SupportPeriodToAdditionalSupportPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupportPeriod",
                table: "SingleSale",
                newName: "AdditionalSupportPeriod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalSupportPeriod",
                table: "SingleSale",
                newName: "SupportPeriod");
        }
    }
}
