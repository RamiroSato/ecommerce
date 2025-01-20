using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioFirstMigraton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
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
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
