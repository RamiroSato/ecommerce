﻿using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.Data.Contexts.Seeds;


namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext : DbContext
    {
        #region Atributos
        public DbSet<Producto>? productos;

        public DbSet<Usuario>? usuarios;

        public DbSet<Roles>? role;
        #endregion 

        #region Propiedades
        public DbSet<Producto>? Productos => productos;

        public DbSet<Usuario>? Usuarios => usuarios;

        public DbSet<Roles>? Roles => Roles;
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
                
                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.Nombre).IsRequired();
                u.Property(u => u.Apellido).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.Property(u => u.IsActive).HasDefaultValue(true);
                //u.Property(u => u.FechaAlta).HasDefaultValue(DateTime.UtcNow);

                u.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);
                

            });

            modelBuilder.Entity<Roles>(r =>
            {

                r.HasKey(r => r.Id);
                r.Property(r => r.Id).ValueGeneratedOnAdd();
                r.Property(r => r.Descripcion).IsRequired();
                r.Property(r => r.Activo).HasDefaultValue(true);
                //r.Property(r => r.FechaAlta).HasDefaultValue(DateTime.UtcNow);

                r.HasMany(r => r.Usuarios)
                 .WithOne(u => u.Rol)
                 .HasForeignKey(u => u.IdRol)
                 .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.ApplyConfiguration(new RolesSeeds());
            //modelBuilder.ApplyConfiguration(new UsuarioSeed());
            //modelBuilder.ApplyConfiguration(new ProductoSeed());
        }
        #endregion

       
    }
}
