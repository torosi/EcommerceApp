using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeVariationMappingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTypeVariationMappings",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    VariationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeVariationMappings", x => new { x.ProductTypeId, x.VariationTypeId });
                    table.ForeignKey(
                        name: "FK_ProductTypeVariationMappings_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypeVariationMappings_VariationTypes_VariationTypeId",
                        column: x => x.VariationTypeId,
                        principalTable: "VariationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeVariationMappings_VariationTypeId",
                table: "ProductTypeVariationMappings",
                column: "VariationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTypeVariationMappings");
        }
    }
}
