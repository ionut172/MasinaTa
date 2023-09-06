using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicitatiiService.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADouaMigrare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kilomegtraj",
                table: "Items",
                newName: "Kilometraj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kilometraj",
                table: "Items",
                newName: "Kilomegtraj");
        }
    }
}
