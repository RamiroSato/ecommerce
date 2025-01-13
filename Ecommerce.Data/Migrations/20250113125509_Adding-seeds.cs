using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addingseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "Id");

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "Id", "Apellido", "CreatedOn", "Email", "IsActive", "Nombre", "Password", "Tipo" },
                values: new object[,]
                {
                    { new Guid("83b00b1f-e914-4b30-8911-0ae9e3af68a3"), "Pérez", new DateTime(2025, 1, 13, 9, 55, 8, 508, DateTimeKind.Local).AddTicks(5296), "juan.perez@example.com", 1, "Juan", "asdasdasd", "Cliente" },
                    { new Guid("ba381ad0-e082-4a91-86b6-27d408aa2509"), "Gómez", new DateTime(2025, 1, 13, 9, 55, 8, 510, DateTimeKind.Local).AddTicks(4874), "maria.gomez@example.com", 1, "María", "kjsdfgk123123", "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("83b00b1f-e914-4b30-8911-0ae9e3af68a3"));

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: new Guid("ba381ad0-e082-4a91-86b6-27d408aa2509"));

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");
        }
    }
}
