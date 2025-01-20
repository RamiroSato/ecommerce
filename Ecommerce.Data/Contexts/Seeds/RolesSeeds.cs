using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Contexts.Seeds
{
    public class RolesSeeds : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder) 
        {

            builder.HasData
                (

                    new Roles
                    {

                        Id = 1,
                        Descripcion = "Admin",


                    },
                    new Roles
                    {

                        Id = 2,
                        Descripcion = "Cliente"

                    }

                );

        
        }

    }
}
