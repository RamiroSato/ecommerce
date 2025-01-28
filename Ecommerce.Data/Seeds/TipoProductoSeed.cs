using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Seeds
{
    public class TipoProductoSeed : IEntityTypeConfiguration<TipoProducto>
    {
        public void Configure(EntityTypeBuilder<TipoProducto> builder)
        {
            builder.HasData(
                new TipoProducto
                {
                    Id = 1,
                    Descripcion = "Remeras",
                },
                new TipoProducto
                {
                    Id = 2,
                    Descripcion = "Pantalones",
                },
                new TipoProducto
                {
                    Id = 3,
                    Descripcion = "Calzado",
                },
                new TipoProducto
                {
                    Id = 4,
                    Descripcion = "Camisas",
                }
                );
        }
    }
}
