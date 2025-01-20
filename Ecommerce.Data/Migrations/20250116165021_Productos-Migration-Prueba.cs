using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductosMigrationPrueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Categoria", "Descripcion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Remeras", "La chomba Lacoste blanca es un ícono de elegancia casual...", 60000, "Chomba Lacoste Blanca" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Pantalones", "Los jeans Levi's azul son un básico imprescindible...", 100000, "Jeans Levi's Azul" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
