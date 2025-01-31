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
                    Id = Guid.Parse("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6a"),
                    CognitoId = "912ba666-a041-702c-91b0-719fc7516934",
                    Nombre = "Admin",
                    Apellido = "Admin",
                    Password = "kjsdfgk123123",
                    Email = "-",
                    IdRol = 1
                },
                new Usuario
                {
                    Id = Guid.Parse("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6b"),
                    CognitoId = "912ba550-a041-703c-91b0-719fc7516931",
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Password = "Abcde123-",
                    Email = "juan.perez@example.com",
                    IdRol = 2

                }
            );
        }
    }
}
