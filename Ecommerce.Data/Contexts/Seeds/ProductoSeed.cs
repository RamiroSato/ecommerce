using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasData(
                new Producto { Id = Guid.NewGuid(), Titulo = "Chomba Lacoste Blanca", Categoria = "Remeras", 
                    Descripcion = "La chomba Lacoste blanca es un ícono de elegancia casual. Confeccionada en algodón premium, ofrece una textura suave y transpirable, perfecta para cualquier ocasión. Su diseño clásico incluye el emblemático logo del cocodrilo bordado en el pecho, cuello tipo polo con botones ajustables y un ajuste regular que se adapta cómodamente a diferentes tipos de cuerpo.", Precio = 60000 },
                new Producto { Id = Guid.NewGuid(), Titulo = "Jeans Levi's Azul", Categoria = "Pantalones", 
                    Descripcion = "Los jeans Levi's azul son un básico imprescindible en cualquier guardarropa. Fabricados con denim de alta calidad, ofrecen una combinación perfecta de durabilidad y comodidad. Su diseño atemporal presenta un corte clásico de cinco bolsillos, cierre de cremallera con botón y el icónico parche de cuero con el logotipo Levi's en la parte trasera.", Precio = 100000 }
                );
        }
    }
}
