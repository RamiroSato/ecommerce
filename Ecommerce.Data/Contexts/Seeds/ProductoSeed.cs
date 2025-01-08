using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasData(
                new Producto
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Titulo = "Chomba Lacoste Blanca",
                    Categoria = "Remeras",
                    Descripcion = "La chomba Lacoste blanca es un ícono de elegancia casual...",
                    Precio = 60000,
                    
                },
                new Producto
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Titulo = "Jeans Levi's Azul",
                    Categoria = "Pantalones",
                    Descripcion = "Los jeans Levi's azul son un básico imprescindible...",
                    Precio = 100000,
                    
                }
            );
        }
    }
}
