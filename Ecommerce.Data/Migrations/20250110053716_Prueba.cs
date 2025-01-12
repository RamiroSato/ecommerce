using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class Prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_WishlistId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "ProductoWishlist",
                columns: table => new
                {
                    ProductosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoWishlist", x => new { x.ProductosId, x.WishlistId });
                    table.ForeignKey(
                        name: "FK_ProductoWishlist_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoWishlist_Wishlist_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoWishlist_WishlistId",
                table: "ProductoWishlist",
                column: "WishlistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoWishlist");

            migrationBuilder.AddColumn<Guid>(
                name: "WishlistId",
                table: "Productos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "WishlistId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "WishlistId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_WishlistId",
                table: "Productos",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id");
        }
    }
}
