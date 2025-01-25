using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.Data.Contexts.Seeds;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext : DbContext
    {
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

            modelBuilder.Entity<Usuario>(u =>
            {
                u.ToTable("usuarios");
                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.Nombre).IsRequired();
                u.Property(u => u.Apellido).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.Property(u => u.IsActive).IsRequired();
                u.Property(u => u.CreatedOn);


            });

            modelBuilder.ApplyConfiguration(new UsuarioSeed());
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
