using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingProductVariationTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skus_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariationOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuId = table.Column<int>(type: "int", nullable: false),
                    VariationTypeId = table.Column<int>(type: "int", nullable: false),
                    VariationValueId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariationOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariationOptions_Skus_SkuId",
                        column: x => x.SkuId,
                        principalTable: "Skus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariationOptions_VariationTypes_VariationTypeId",
                        column: x => x.VariationTypeId,
                        principalTable: "VariationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariationOptions_VariationValues_VariationValueId",
                        column: x => x.VariationValueId,
                        principalTable: "VariationValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_SkuId",
                table: "ProductVariationOptions",
                column: "SkuId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_VariationTypeId",
                table: "ProductVariationOptions",
                column: "VariationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariationOptions_VariationValueId",
                table: "ProductVariationOptions",
                column: "VariationValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_ProductId",
                table: "Skus",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariationOptions");

            migrationBuilder.DropTable(
                name: "Skus");
        }
    }
}
