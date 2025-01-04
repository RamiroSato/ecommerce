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
        #region Atributos
        public DbSet<Producto>? productos;
        #endregion

        #region Propiedades
        public DbSet<Producto>? Productos => productos;
        #endregion

        #region Constructor
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.Property(p => p.Titulo);
                p.Property(p => p.Categoria);
                p.Property(p => p.Descripcion);
                p.Property(p => p.Precio);
            });

            modelBuilder.ApplyConfiguration(new ProductoSeed());
        }
        #endregion
    }
}
