using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;
using Ecommerce.Data.Seeds;


namespace Ecommerce.Data.Contexts
{
    public class EcommerceContext(DbContextOptions<EcommerceContext> options) : DbContext(options)
    {
        #region Propiedades

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Rol>? Roles { get; set; }
        public DbSet<TipoProducto>? TipoProductos { get; set; }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Lote>? Lotes { get; set; }
        public DbSet<Wishlist>? Wishlists { get; set; }

        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Rol
            modelBuilder.Entity<Rol>(r =>
            {
                r.ToTable("Roles");
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
            #endregion

            #region Usuario
            modelBuilder.Entity<Usuario>(u =>
            {

                u.HasKey(u => u.Id);
                u.Property(u => u.Id).ValueGeneratedOnAdd();
                u.Property(u => u.IdRol).IsRequired();
                u.Property(u => u.Nombre).IsRequired();
                u.Property(u => u.Apellido).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.Property(u => u.Email).IsRequired();
                u.HasIndex(u => u.Email).IsUnique();
                u.Property(u => u.Activo).HasDefaultValue(true);
                u.Property(u => u.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

                u.HasOne(u => u.Wishlist)
                .WithOne(w => w.Usuario)
                .HasForeignKey<Wishlist>(w => w.IdUsuario);

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

            #region PreOrden
            modelBuilder.Entity<PreOrden>(PO =>
            {
                PO.HasKey(PO => PO.Id);
                PO.Property(PO => PO.Id).ValueGeneratedOnAdd();
                PO.Property(PO => PO.IdUsuario).IsRequired();
                PO.Property(PO => PO.Descripcion).IsRequired();
                PO.Property(PO => PO.Vencimiento).IsRequired();
                PO.Property(PO => PO.Activo).IsRequired().HasDefaultValue(true);
                PO.Property(PO => PO.FechaAlta).IsRequired();

                PO.HasOne(PO => PO.Usuario)
                .WithMany()
                .HasForeignKey(PO => PO.IdUsuario);

                PO.HasOne(PO => PO.Orden)
                .WithOne(O => O.PreOrden)
                .HasForeignKey<Orden>(O => O.IdPreOrden);

                PO.HasOne(PO => PO.Transaccion)
                .WithOne(T => T.PreOrden)
                .HasForeignKey<Orden>(T => T.IdPreOrden);
            });
            #endregion

            #region ItemPreOrden
            modelBuilder.Entity<ItemPreOrden>(IPO =>
            {
                IPO.HasKey(IPO => IPO.Id);
                IPO.Property(IPO => IPO.Id).ValueGeneratedOnAdd();
                IPO.Property(IPO => IPO.IdLote).IsRequired();
                IPO.Property(IPO => IPO.IdPreOrden).IsRequired();
                IPO.Property(IPO => IPO.PrecioUnitario).HasConversion<decimal>().IsRequired();
                IPO.Property(IPO => IPO.Cantidad).IsRequired();
                IPO.Property(IPO => IPO.Activo).IsRequired().HasDefaultValue(true);
                IPO.Property(IPO => IPO.FechaAlta).IsRequired().HasDefaultValueSql("GETDATE()");

                IPO.HasOne(IPO => IPO.PreOrden)
                .WithMany(PO => PO.Items)
                .HasForeignKey(IPO => IPO.IdPreOrden);

                IPO.HasOne(IPO => IPO.Lote)
                .WithMany()
                .HasForeignKey(IPO => IPO.IdLote);
            });
            #endregion

            #region Orden
            modelBuilder.Entity<Orden>(O =>
            {
                O.HasKey(O => O.Id);
                O.Property(O => O.Id).ValueGeneratedOnAdd();
                O.Property(O => O.IdPreOrden).IsRequired();
                O.Property(O => O.NumeroOrden).IsRequired();
                O.Property(O => O.FechaEntrega);
                O.Property(O => O.Activo).IsRequired().HasDefaultValue(true);
                O.Property(O => O.FechaAlta).IsRequired();


            });
            #endregion

            #region Transaccion

            modelBuilder.Entity<Transaccion>(T =>
            {
                T.HasKey(T => T.Id);
                T.Property(T => T.Id).ValueGeneratedOnAdd();
                T.Property(T => T.IdPreOrden).IsRequired();
                T.Property(T => T.FechaPago).IsRequired();
                T.Property(T => T.Monto).HasConversion<decimal>().IsRequired();
                T.Property(T => T.NombreTarjeta).IsRequired();
                T.Property(T => T.NumeroTarjeta).IsRequired();
                T.Property(T => T.Activo).IsRequired().HasDefaultValue(true);
                T.Property(T => T.FechaAlta).IsRequired();
            });

            #endregion

            #region Seeds
            modelBuilder.ApplyConfiguration(new RolesSeeds());
            modelBuilder.ApplyConfiguration(new TipoProductoSeed());
            modelBuilder.ApplyConfiguration(new UsuarioSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
            #endregion
        }

        #endregion

    }
}
