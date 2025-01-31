using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.Data.Contexts.Seeds;


namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext(DbContextOptions<EcommerceContext> options) : DbContext(options)
    {
        #region Propiedades

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoProducto> TipoProductos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
<<<<<<< HEAD
            #region Roles
=======
            #region Rol
>>>>>>> nico
            modelBuilder.Entity<Rol>(r =>
            {
                r.ToTable("Roles");
                r.HasKey(r => r.Id);
                r.Property(r => r.Id).ValueGeneratedOnAdd();

                r.Property(r => r.Descripcion).IsRequired();
                r.Property(r => r.Activo).HasDefaultValue(true);
                r.Property(r => r.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

            });
            #endregion

<<<<<<< HEAD
            #region Usuarios
=======
            #region Usuario
>>>>>>> nico
            modelBuilder.Entity<Usuario>(u =>
            {

                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.IdRol).IsRequired();
                u.Property(u => u.CognitoId).IsRequired();
                u.Property(u => u.Nombre).IsRequired();
                u.Property(u => u.Apellido).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.HasIndex(u => u.Email).IsUnique();
                u.Property(u => u.Activo).HasDefaultValue(true);
                u.Property(u => u.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");
<<<<<<< HEAD
=======

                u.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

                u.HasOne(u => u.Wishlist)
                .WithOne(w => w.Usuario)
                .HasForeignKey<Wishlist>(w => w.IdUsuario);

>>>>>>> nico
            });
            #endregion

            #region TiposProducto
            modelBuilder.Entity<TipoProducto>(tp =>
            {
                tp.HasKey(tp => tp.Id);
                tp.Property(tp => tp.Descripcion).IsRequired();
                tp.Property(tp => tp.Activo).IsRequired();
                tp.Property(tp => tp.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");
            });
<<<<<<< HEAD
=======

>>>>>>> nico
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
                .WithMany(w => w.Productos)
                .UsingEntity(wp => wp.ToTable("WishlistsProductos"));
            });
            #endregion

            #region Wishlist
            modelBuilder.Entity<Wishlist>(w =>
            {
                w.HasKey(w => w.Id);
                w.Property(w => w.Id).ValueGeneratedOnAdd();
                w.Property(w => w.IdUsuario).IsRequired();

                w.HasMany(w => w.Productos)
                .WithMany(p => p.Wishlists);
            });
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

            #region Seeds
            modelBuilder.ApplyConfiguration(new RolesSeeds());
            modelBuilder.ApplyConfiguration(new TipoProductoSeed());
            modelBuilder.ApplyConfiguration(new UsuarioSeed());
            modelBuilder.ApplyConfiguration(new TipoProductoSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
            #endregion
        }

        #endregion
    }
}
