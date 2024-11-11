using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariationOptions_SkuId_VariationTypeId_VariationValueId",
                table: "ProductVariationOptions");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_SkuId_VariationTypeId",
                table: "ProductVariationOptions",
                columns: new[] { "SkuId", "VariationTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariationOptions_SkuId_VariationTypeId",
                table: "ProductVariationOptions");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_SkuId_VariationTypeId_VariationValueId",
                table: "ProductVariationOptions",
                columns: new[] { "SkuId", "VariationTypeId", "VariationValueId" },
                unique: true);
        }
    }
}
