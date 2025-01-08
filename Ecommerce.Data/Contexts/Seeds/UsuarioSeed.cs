using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class UsuarioSeed : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(
                new Usuario
                {
                    Id = Guid.Parse("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"),
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@example.com",
                    Tipo = "Cliente",
                    
                },
                new Usuario
                {
                    Id = Guid.Parse("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"),
                    Nombre = "María",
                    Apellido = "Gómez",
                    Email = "maria.gomez@example.com",
                    Tipo = "Administrador",
                   
                }
            );
        }
    }
}
