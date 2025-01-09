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

                    b.Property<Guid?>("WishlistId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WishlistId");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Categoria = "Remeras",
                            Descripcion = "La chomba Lacoste blanca es un ícono de elegancia casual...",
                            Precio = 60000,
                            Titulo = "Chomba Lacoste Blanca"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Categoria = "Pantalones",
                            Descripcion = "Los jeans Levi's azul son un básico imprescindible...",
                            Precio = 100000,
                            Titulo = "Jeans Levi's Azul"
                        });
                });

            modelBuilder.Entity("Ecommerce.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f1a4d5a6-7c2b-4eab-9e47-8c6b3f4c7f81"),
                            Apellido = "Pérez",
                            Email = "juan.perez@example.com",
                            Nombre = "Juan",
                            Password = "",
                            Tipo = "Cliente"
                        },
                        new
                        {
                            Id = new Guid("f2b5d6a7-8d3c-5fab-0e58-9d7c4d5a8f92"),
                            Apellido = "Gómez",
                            Email = "maria.gomez@example.com",
                            Nombre = "María",
                            Password = "",
                            Tipo = "Administrador"
                        });
                });

            modelBuilder.Entity("Ecommerce.Models.Wishlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("Ecommerce.Models.Producto", b =>
                {
                    b.HasOne("Ecommerce.Models.Wishlist", "Wishlist")
                        .WithMany()
                        .HasForeignKey("WishlistId");

                    b.Navigation("Wishlist");
                });
#pragma warning restore 612, 618
        }
    }
}
