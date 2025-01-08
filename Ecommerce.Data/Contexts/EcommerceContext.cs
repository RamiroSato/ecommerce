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

        public DbSet<Usuario>? usuarios;
        #endregion 

        #region Propiedades
        public DbSet<Producto>? Productos => productos;

        public DbSet<Usuario>? Usuarios => usuarios; 
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

            modelBuilder.Entity<Usuario>(u =>
            {
                u.ToTable("Usuarios");
                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.Nombre);
                u.Property(u => u.Apellido);
                u.Property(u => u.Password);
                u.Property(u => u.Email);


            });

            modelBuilder.ApplyConfiguration(new UsuarioSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
        }
        #endregion
    }
}
