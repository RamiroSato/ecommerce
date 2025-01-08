using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixingseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Wishlist_WishlistId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_WishlistId",
                table: "Usuarios",
                newName: "IX_Usuarios_WishlistId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WishlistId",
                table: "Productos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "WishlistId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Categoria", "Descripcion", "Precio", "Titulo", "WishlistId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Remeras", "La chomba Lacoste blanca es un ícono de elegancia casual...", 60000, "Chomba Lacoste Blanca", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Pantalones", "Los jeans Levi's azul son un básico imprescindible...", 100000, "Jeans Levi's Azul", null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Email", "Nombre", "Tipo", "WishlistId" },
                values: new object[,]
                {
                    { new Guid("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"), "Pérez", "juan.perez@example.com", "Juan", "Cliente", null },
                    { new Guid("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"), "Gómez", "maria.gomez@example.com", "María", "Administrador", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Wishlist_WishlistId",
                table: "Usuarios",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Wishlist_WishlistId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"));

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_WishlistId",
                table: "Usuario",
                newName: "IX_Usuario_WishlistId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WishlistId",
                table: "Productos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WishlistId",
                table: "Usuario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Wishlist_WishlistId",
                table: "Productos",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Wishlist_WishlistId",
                table: "Usuario",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
