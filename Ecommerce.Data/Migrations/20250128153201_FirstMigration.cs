using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProductos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    CognitoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_TipoProductos_IdTipoProducto",
                        column: x => x.IdTipoProducto,
                        principalTable: "TipoProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistsProductos",
                columns: table => new
                {
                    ProductosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishlistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistsProductos", x => new { x.ProductosId, x.WishlistsId });
                    table.ForeignKey(
                        name: "FK_WishlistsProductos_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistsProductos_Wishlists_WishlistsId",
                        column: x => x.WishlistsId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "TipoProductos",
                columns: new[] { "Id", "Activo", "Descripcion" },
                values: new object[,]
                {
                    { 1, false, "Remeras" },
                    { 2, false, "Pantalones" },
                    { 3, false, "Calzado" },
                    { 4, false, "Camisas" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Descripcion", "IdTipoProducto", "Imagen", "Precio" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Chomba Lacoste Blanca: es un ícono de elegancia casual. Confeccionada en algodón premium, ofrece una textura suave y transpirable, perfecta para cualquier ocasión. Su diseño clásico incluye el emblemático logo del cocodrilo bordado en el pecho, cuello tipo polo con botones ajustables y un ajuste regular que se adapta cómodamente a diferentes tipos de cuerpo.", 1, "https://caradec-lorefice-sato-bucket.s3.us-east-2.amazonaws.com/c451baf0-fd10-401c-9c3b-5ea3d79a6420.jpg", 60000m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Jeans Levi's Azul: son un básico imprescindible en cualquier guardarropa. Fabricados con denim de alta calidad, ofrecen una combinación perfecta de durabilidad y comodidad. Su diseño atemporal presenta un corte clásico de cinco bolsillos, cierre de cremallera con botón y el icónico parche de cuero con el logotipo Levi's en la parte trasera.", 2, "https://caradec-lorefice-sato-bucket.s3.us-east-2.amazonaws.com/f10695c4-85f7-4768-8d41-819b3dd868ce.jpg", 100000m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Zapatillas Hush Puppies Marrones: ofrecen una combinación perfecta de estilo y comodidad. Fabricadas con materiales de alta calidad, presentan un diseño casual y versátil ideal para el día a día. Su suela antideslizante y su plantilla acolchada garantizan soporte y confort durante todo el día. Perfectas para combinar con jeans o pantalones chinos, estas zapatillas son el complemento ideal para un look relajado pero elegante.", 3, "https://caradec-lorefice-sato-bucket.s3.us-east-2.amazonaws.com/c0e51c96-4bb5-4162-b4cd-fd116aa17909.jpg", 850000m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Camisa Denim Verde: camisa formal que redefine la elegancia casual con un toque moderno. Confeccionada en tela de mezclilla de alta calidad, ofrece un acabado refinado que la convierte en la opción perfecta para ocasiones formales o semi-formales. Su corte ajustado y detalles clásicos, como el cuello rígido y los botones en tono contrastante, aseguran un estilo impecable. Combínala con pantalones oscuros para un look profesional o con chinos para un outfit más relajado pero sofisticado.\r\n\r\n", 4, "https://caradec-lorefice-sato-bucket.s3.us-east-2.amazonaws.com/d1a844bf-fda5-4d13-9e0d-f62bfeabdfba.jpg", 54200m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "CognitoId", "Email", "IdRol", "Nombre", "Password" },
                values: new object[,]
                {
                    { new Guid("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6a"), "Admin", "912ba666-a041-702c-91b0-719fc7516934", "-", 1, "Admin", "kjsdfgk123123" },
                    { new Guid("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6b"), "Pérez", "912ba550-a041-703c-91b0-719fc7516931", "juan.perez@example.com", 2, "Juan", "Abcde123-" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_IdProducto",
                table: "Lotes",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdTipoProducto",
                table: "Productos",
                column: "IdTipoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_IdUsuario",
                table: "Wishlists",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistsProductos_WishlistsId",
                table: "WishlistsProductos",
                column: "WishlistsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "WishlistsProductos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "TipoProductos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
