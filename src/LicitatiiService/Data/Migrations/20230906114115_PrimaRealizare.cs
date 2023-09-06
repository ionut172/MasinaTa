using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicitatiiService.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimaRealizare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licitatii",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PretRezervare = table.Column<int>(type: "integer", nullable: false),
                    Vanzator = table.Column<string>(type: "text", nullable: true),
                    Castigator = table.Column<string>(type: "text", nullable: true),
                    CelMaiMareBid = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LicitatieEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: true),
                    ModelMasina = table.Column<string>(type: "text", nullable: true),
                    An = table.Column<int>(type: "integer", nullable: false),
                    Culoare = table.Column<string>(type: "text", nullable: true),
                    Kilomegtraj = table.Column<int>(type: "integer", nullable: false),
                    ImagineUrl = table.Column<string>(type: "text", nullable: true),
                    LicitatieId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Licitatii_LicitatieId",
                        column: x => x.LicitatieId,
                        principalTable: "Licitatii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LicitatieId",
                table: "Items",
                column: "LicitatieId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Licitatii");
        }
    }
}
