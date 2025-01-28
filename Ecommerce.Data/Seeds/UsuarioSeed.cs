using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Seeds
{
    public class UsuarioSeed : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(
                new Usuario
                {
                    Id = Guid.Parse("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6a"),
                    Nombre = "Admin",
                    Apellido = "Admin",
                    Password = "kjsdfgk123123",
                    Email = "-",
                    IdRol = 1
                },
                new Usuario
                {
                    Id = Guid.Parse("f4b3e2b5-7b1e-4e1e-8b41-5b9e3e4f1e6b"),
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Password = "asdasdasd",
                    Email = "juan.perez@example.com",
                    IdRol = 2

                }
            );
        }
    }
}
