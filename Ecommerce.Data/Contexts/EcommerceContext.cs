using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<Producto>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.Property(p => p.Titulo);
                p.Property(p => p.Categoria);
                p.Property(p => p.Descripcion);
                p.Property(p => p.Precio);
            });


            modelBuilder.ApplyConfiguration(new RolesSeeds());
            //modelBuilder.ApplyConfiguration(new UsuarioSeed());
            //modelBuilder.ApplyConfiguration(new ProductoSeed());
        }
        #endregion


    }
}
