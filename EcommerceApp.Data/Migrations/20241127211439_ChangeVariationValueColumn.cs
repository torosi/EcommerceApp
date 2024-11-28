using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVariationValueColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariationOptions_VariationValues_VariationValueId",
                table: "ProductVariationOptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariationOptions_VariationValueId",
                table: "ProductVariationOptions");

            migrationBuilder.DropColumn(
                name: "VariationValueId",
                table: "ProductVariationOptions");

            migrationBuilder.AddColumn<string>(
                name: "VariationValue",
                table: "ProductVariationOptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariationValue",
                table: "ProductVariationOptions");

            migrationBuilder.AddColumn<int>(
                name: "VariationValueId",
                table: "ProductVariationOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_VariationValueId",
                table: "ProductVariationOptions",
                column: "VariationValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariationOptions_VariationValues_VariationValueId",
                table: "ProductVariationOptions",
                column: "VariationValueId",
                principalTable: "VariationValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
