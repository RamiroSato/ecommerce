using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWishlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoWishlist_Wishlist_WishlistId",
                table: "ProductoWishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist");

            migrationBuilder.RenameTable(
                name: "Wishlist",
                newName: "Wishlists");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "ProductoWishlist",
                newName: "WishlistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoWishlist_WishlistId",
                table: "ProductoWishlist",
                newName: "IX_ProductoWishlist_WishlistsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoWishlist_Wishlists_WishlistsId",
                table: "ProductoWishlist",
                column: "WishlistsId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoWishlist_Wishlists_WishlistsId",
                table: "ProductoWishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                table: "Wishlists");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                newName: "Wishlist");

            migrationBuilder.RenameColumn(
                name: "WishlistsId",
                table: "ProductoWishlist",
                newName: "WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoWishlist_WishlistsId",
                table: "ProductoWishlist",
                newName: "IX_ProductoWishlist_WishlistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoWishlist_Wishlist_WishlistId",
                table: "ProductoWishlist",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
