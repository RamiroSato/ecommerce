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
                    CognitoId = "e17bb540-1001-705b-ecb6-42c780157de1",
                    Nombre = "Admin",
                    Apellido = "Admin",
                    Password = "m19oaVI3BqvY+u0UTODe/OtXhmlcysjSsAi/yPGf5gc=",
                    Email = "satoramirodev+adminecommerce@gmail.com",
                    IdRol = 1
                },
                new Usuario
                {
                    Id = Guid.Parse("4f892c22-903b-469b-34de-08dd40dbec02"),
                    IdRol = 1,
                    CognitoId = "a1ab2500-90f1-7031-0fe8-040f4cb9ca6f",
                    Nombre = "Ramiro",
                    Apellido = "Sato",
                    Password = "FEjejD8NNYoAX4bxGfftXTuh/vot6HKIO2rk29gXRCM=",
                    Email = "satoramirodev@gmail.com"
                },
                new Usuario
                {
                    Id = Guid.Parse("fed99fa7-c058-4958-1fe6-08dd42d0b35f"),
                    IdRol = 2,
                    CognitoId = "819b4550-a071-7055-250d-fda6f1d6351a",
                    Nombre = "Mariana",
                    Apellido = "Caradec",
                    Password = "b1Qt0lD5brjIRUrdPDGsu8F7zArS4JIhqkZtrdtrJF8=",
                    Email = "maricaradec@gmail.com",
                }
            );
        }
    }
}
