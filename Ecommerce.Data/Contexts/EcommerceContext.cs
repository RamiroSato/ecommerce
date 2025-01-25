﻿using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.Data.Contexts.Seeds;


namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext : DbContext
    {
        #region Atributos

        DbSet<Usuario>? usuarios;
        DbSet<Roles>? roles;
        DbSet<Producto>? productos;


        #endregion

        #region Propiedades
        public DbSet<Producto> Productos
        {
            get
            {
                if (productos == null)
                    throw new Exception("No existe productos");
                return productos;
            }
            set => productos = value;
        }

        public DbSet<Usuario> Usuarios
        {
            get
            {
                if (usuarios == null)
                    throw new Exception("No existe usuarios");
                return usuarios;
            }
            set => usuarios = value;
        }

        public DbSet<Roles> Roles
        {
            get
            {
                if (roles == null)
                    throw new Exception("No existe roles");

                return roles;
            }
            set => roles = value;
        }

        #region Propiedades
        public DbSet<TipoProducto>? TipoProductos { get; set; }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Lote>? Lotes { get; set; }
        public DbSet<Wishlist>? Wishlists { get; set; }
        #endregion

        #region Constructor
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Roles>(r =>
            {

                r.HasKey(r => r.Id);
                r.Property(r => r.Id).ValueGeneratedOnAdd();

                r.Property(r => r.Descripcion).IsRequired();
                r.Property(r => r.Activo).HasDefaultValue(true);
                r.Property(r => r.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

                r.HasMany(r => r.Usuarios)
                 .WithOne(u => u.Rol)
                 .HasForeignKey(u => u.IdRol)
                 .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Usuario>(u =>
            {

                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.IdRol).IsRequired();
                u.Property(u => u.Nombre).IsRequired();
                u.Property(u => u.Apellido).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.HasIndex(u=> u.Email).IsUnique();
                u.Property(u => u.Activo).HasDefaultValue(true);
                u.Property(u => u.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");


            });

            #region TiposProducto
            modelBuilder.Entity<TipoProducto>(tp =>
            {
                tp.HasKey(tp => tp.Id);
                tp.Property(tp => tp.Descripcion).IsRequired();
                tp.Property(tp => tp.Activo).IsRequired();
                tp.Property(tp => tp.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.ApplyConfiguration(new TipoProductoSeed());
            #endregion

            #region Productos
            modelBuilder.Entity<Producto>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.Property(p => p.IdTipoProducto).IsRequired();
                p.Property(p => p.Imagen).IsRequired();
                p.Property(p => p.Descripcion).IsRequired();
                p.Property(p => p.Precio).HasColumnType("DECIMAL").IsRequired();
                p.Property(p => p.Activo).IsRequired().HasDefaultValue(true);
                p.Property(p => p.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

                p.HasOne(p => p.TipoProducto)
                .WithMany(tp => tp.Productos)
                .HasForeignKey(p => p.IdTipoProducto);

                p.HasMany(p => p.Wishlists)
                .WithMany(w => w.Productos);
            });


            modelBuilder.ApplyConfiguration(new RolesSeeds());
            //modelBuilder.ApplyConfiguration(new UsuarioSeed());
            //modelBuilder.ApplyConfiguration(new ProductoSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
            #endregion

            #region Lotes
            modelBuilder.Entity<Lote>(l =>
            {
                l.HasKey(l => l.Id);
                l.Property(l => l.Id).ValueGeneratedOnAdd();
                l.Property(l => l.Descripcion).IsRequired();
                l.Property(l => l.Cantidad).IsRequired();
                l.Property(l => l.Activo).IsRequired().HasDefaultValue(true);
                l.Property(l => l.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

                l.HasOne(l => l.Producto)
                .WithMany(p => p.Lotes)
                .HasForeignKey(l => l.IdProducto);
            });
            #endregion
        }
        #endregion


    }
}
