using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationWithSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Categoria", "Descripcion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Remeras", "La chomba Lacoste blanca es un ícono de elegancia casual...", 60000, "Chomba Lacoste Blanca" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Pantalones", "Los jeans Levi's azul son un básico imprescindible...", 100000, "Jeans Levi's Azul" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Email", "Nombre", "Password", "Tipo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Pérez", "juan.perez@example.com", "Juan", "asdasdasd", "Cliente" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Gómez", "maria.gomez@example.com", "María", "kjsdfgk123123", "Administrador" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Lorefice", "nicolaslorefice@gmail.com", "Nicolas", "Abcde123", "Cliente" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
