using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Data.Contexts.Seeds;

namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext : DbContext
    {
        #region Propiedades
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Wishlist>? Wishlists { get; set; }
        #endregion

        #region Constructor
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Productos
            modelBuilder.Entity<Producto>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.Property(p => p.Titulo).IsRequired();
                p.Property(p => p.Categoria).IsRequired();
                p.Property(p => p.Descripcion).IsRequired();
                p.Property(p => p.Precio).IsRequired();
                p.HasMany(p => p.Wishlists)
                .WithMany(w => w.Productos);
            });

            modelBuilder.ApplyConfiguration(new ProductoSeed());
            #endregion
        }
        #endregion
    }
}
