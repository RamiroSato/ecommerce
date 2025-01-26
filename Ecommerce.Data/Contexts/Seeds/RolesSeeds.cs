using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class RolesSeeds : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder) 
        {
            builder.HasData
                (

                    new Rol
                    {

                        Id = 1,
                        Descripcion = "Admin",


                    },
                    new Rol
                    {

                        Id = 2,
                        Descripcion = "Cliente"

                    }

                );

        
        }

    }
}
