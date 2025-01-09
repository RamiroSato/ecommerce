using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class Semodificalatabladeusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Wishlist_WishlistId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_WishlistId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"),
                column: "Password",
                value: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"),
                column: "Password",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuarios");

            migrationBuilder.AddColumn<Guid>(
                name: "WishlistId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"),
                column: "WishlistId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"),
                column: "WishlistId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_WishlistId",
                table: "Usuarios",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Wishlist_WishlistId",
                table: "Usuarios",
                column: "WishlistId",
                principalTable: "Wishlist",
                principalColumn: "Id");
        }
    }
}
