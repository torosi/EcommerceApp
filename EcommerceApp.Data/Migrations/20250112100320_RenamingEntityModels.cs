using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingEntityModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeVariationMappings_ProductTypes_ProductTypeId",
                table: "ProductTypeVariationMappings");

            migrationBuilder.AddColumn<int>(
                name: "ProcuctTypeId",
                table: "ProductTypeVariationMappings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeVariationMappings_ProcuctTypeId",
                table: "ProductTypeVariationMappings",
                column: "ProcuctTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeVariationMappings_ProductTypes_ProcuctTypeId",
                table: "ProductTypeVariationMappings",
                column: "ProcuctTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypeVariationMappings_ProductTypes_ProcuctTypeId",
                table: "ProductTypeVariationMappings");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypeVariationMappings_ProcuctTypeId",
                table: "ProductTypeVariationMappings");

            migrationBuilder.DropColumn(
                name: "ProcuctTypeId",
                table: "ProductTypeVariationMappings");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTypeVariationMappings_ProductTypes_ProductTypeId",
                table: "ProductTypeVariationMappings",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
