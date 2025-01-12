﻿// <auto-generated />
using System;
using Ecommerce.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    partial class EcommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Models.Producto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Categoria = "Remeras",
                            Descripcion = "La chomba Lacoste blanca es un ícono de elegancia casual. Confeccionada en algodón premium, ofrece una textura suave y transpirable, perfecta para cualquier ocasión. Su diseño clásico incluye el emblemático logo del cocodrilo bordado en el pecho, cuello tipo polo con botones ajustables y un ajuste regular que se adapta cómodamente a diferentes tipos de cuerpo.",
                            Precio = 60000,
                            Titulo = "Chomba Lacoste Blanca"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Categoria = "Pantalones",
                            Descripcion = "Los jeans Levi's azul son un básico imprescindible en cualquier guardarropa. Fabricados con denim de alta calidad, ofrecen una combinación perfecta de durabilidad y comodidad. Su diseño atemporal presenta un corte clásico de cinco bolsillos, cierre de cremallera con botón y el icónico parche de cuero con el logotipo Levi's en la parte trasera.",
                            Precio = 100000,
                            Titulo = "Jeans Levi's Azul"
                        });
                });

            modelBuilder.Entity("Ecommerce.Models.Wishlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("ProductoWishlist", b =>
                {
                    b.Property<Guid>("ProductosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WishlistsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductosId", "WishlistsId");

                    b.HasIndex("WishlistsId");

                    b.ToTable("ProductoWishlist");
                });

            modelBuilder.Entity("ProductoWishlist", b =>
                {
                    b.HasOne("Ecommerce.Models.Producto", null)
                        .WithMany()
                        .HasForeignKey("ProductosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Models.Wishlist", null)
                        .WithMany()
                        .HasForeignKey("WishlistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
