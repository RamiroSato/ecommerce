using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasData(
                new Producto { 
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                    IdTipoProducto = 1, 
                    Descripcion = "Chomba Lacoste Blanca: es un ícono de elegancia casual. Confeccionada en algodón premium, ofrece una textura suave y transpirable, perfecta para cualquier ocasión. Su diseño clásico incluye el emblemático logo del cocodrilo bordado en el pecho, cuello tipo polo con botones ajustables y un ajuste regular que se adapta cómodamente a diferentes tipos de cuerpo.", 
                    Precio = 60000,
                },
                new Producto
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    IdTipoProducto = 2,
                    Descripcion = "Jeans Levi's Azul: son un básico imprescindible en cualquier guardarropa. Fabricados con denim de alta calidad, ofrecen una combinación perfecta de durabilidad y comodidad. Su diseño atemporal presenta un corte clásico de cinco bolsillos, cierre de cremallera con botón y el icónico parche de cuero con el logotipo Levi's en la parte trasera.",
                    Precio = 100000,
                },
                new Producto
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    IdTipoProducto = 3,
                    Descripcion = "Zapatillas Hush Puppies Marrones: ofrecen una combinación perfecta de estilo y comodidad. Fabricadas con materiales de alta calidad, presentan un diseño casual y versátil ideal para el día a día. Su suela antideslizante y su plantilla acolchada garantizan soporte y confort durante todo el día. Perfectas para combinar con jeans o pantalones chinos, estas zapatillas son el complemento ideal para un look relajado pero elegante.",
                    Precio = 850000,
                },
                new Producto
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    IdTipoProducto = 4,
                    Descripcion = "Camisa Denim Verde: camisa formal que redefine la elegancia casual con un toque moderno. Confeccionada en tela de mezclilla de alta calidad, ofrece un acabado refinado que la convierte en la opción perfecta para ocasiones formales o semi-formales. Su corte ajustado y detalles clásicos, como el cuello rígido y los botones en tono contrastante, aseguran un estilo impecable. Combínala con pantalones oscuros para un look profesional o con chinos para un outfit más relajado pero sofisticado.\r\n\r\n",
                    Precio = 54200,
                }
                );
        }
    }
}
