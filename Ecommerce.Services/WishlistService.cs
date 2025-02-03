using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.DTO;
using Ecommerce.Data;
using Ecommerce.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class WishlistService(EcommerceContext context) : IWishlistService
{
    readonly EcommerceContext _context = context;
    readonly DbSet<Wishlist> _wishlists = context.Wishlists;
    readonly DbSet<Usuario> _usuarios = context.Usuarios;
    readonly DbSet<Producto> _productos = context.Productos;

    private async Task<bool> CheckCognitoId(string? requestCognitoId, string wishlistCognitoId)
    {
        var usuarioCognito = await _usuarios
            .FirstOrDefaultAsync(u => u.CognitoId == requestCognitoId)
            ?? throw new UnauthorizedAccessException("You are not authorized to modify this user.");

        if (wishlistCognitoId != requestCognitoId && usuarioCognito.IdRol != 1)
            throw new UnauthorizedAccessException("You are not authorized to modify this user.");

        return true;
    }


    public async Task<WishlistDTO> AddProduct(Guid idWishlist, Guid idProducto, string? requestCognitoId)
    {
        var wishlist = await _wishlists
            .Include(w => w.Usuario)
            .Include(w => w.Productos)
                .ThenInclude(p => p.TipoProducto)
            .Where(w => w.Id == idWishlist)
            .FirstOrDefaultAsync()
            ?? throw new ResourceNotFoundException("Wishlist not found");

        var producto = await _productos
            .Include(p => p.TipoProducto)
            .Where(p => p.Id == idProducto)
            .FirstOrDefaultAsync()
            ?? throw new ResourceNotFoundException("Product not found");

        await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

        if (!wishlist.Usuario.Activo)
        {
            throw new ResourceNotFoundException("Wishlist's User not available");
        }

        if (!producto.Activo)
        {
            throw new ResourceNotFoundException("Product not available");
        }

        if (wishlist.Productos.Any(p => p.Id == idProducto))
        {
            throw new ResourceNotFoundException("Product already in wishlist");
        }

        wishlist.Productos.Add(producto);


        await _context.SaveChangesAsync();

        return new()
        {
            Id = wishlist.Id,
            Productos = [..
                wishlist.Productos?.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProducto = p.TipoProducto.Descripcion,
                    Imagen = p.Imagen,
                    Precio = p.Precio,
                    Activo = p.Activo
                }).ToList()
            ]
        };
    }

    public async Task<Wishlist> CreateWishlist(Guid idUsuario, string? requestCognitoId)
    {
        var query = _usuarios.AsQueryable()
            .Include(u => u.Wishlist)
            .Where(u => u.Id == idUsuario);

        var usuario = await query.FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("User not found");

        await CheckCognitoId(requestCognitoId, usuario.CognitoId);

        if (!usuario.Activo)
        {
            throw new ResourceNotFoundException("User not available");
        }

        if (usuario.Wishlist != null)
        {
            throw new ResourceAlreadyExistsException("User already has a wishlist");
        }

        var wishlist = new Wishlist
        {
            IdUsuario = idUsuario,
            Usuario = usuario,
            Productos = []
        };

        _wishlists.Add(wishlist);
        await _context.SaveChangesAsync();
        return wishlist;
    }

    public async Task<bool> DeleteWishlist(Guid id, string? requestCognitoId)
    {
        var wishlist = await _wishlists.FindAsync(id) ?? throw new ResourceNotFoundException("Wishlist not found");

        await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

        _wishlists.Remove(wishlist);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<WishlistDTO> GetWishlist(Guid? id, Guid? idUsuario, string? requestCognitoId)
    {
        if (id == null && idUsuario == null)
            throw new ArgumentException("At least one of id or idUsuario must be provided");

        if (id != null && idUsuario != null)
            throw new ArgumentException("Only one of id or idUsuario must be provided");

        var wishlist = await _wishlists
            .Include(w => w.Usuario)
            .Include(w => w.Productos)
                .ThenInclude(p => p.TipoProducto)
            .Where(w => w.Id == id || w.IdUsuario == idUsuario)
            .FirstOrDefaultAsync()
            ?? throw new ResourceNotFoundException("Wishlist not found");

        await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

        return new()
        {
            Id = wishlist.Id,
            Productos = [..
                wishlist.Productos?.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProducto = p.TipoProducto.Descripcion,
                    Imagen = p.Imagen,
                    Precio = p.Precio,
                    Activo = p.Activo
                }).ToList()
            ]
        };
    }

    public async Task<WishlistDTO> RemoveProduct(Guid idWishlist, Guid idProducto, string? requestCognitoId)
    {
        var wishlist = await _wishlists
            .Include(w => w.Usuario)
            .Include(w => w.Productos)
                .ThenInclude(p => p.TipoProducto)
            .Where(w => w.Id == idWishlist)
            .FirstOrDefaultAsync()
            ?? throw new ResourceNotFoundException("Wishlist not found");
        var producto = await _productos.FindAsync(idProducto) ?? throw new ResourceNotFoundException("Product not found");

        await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

        if (!wishlist.Productos.Any(p => p.Id == idProducto))
        {
            throw new ResourceNotFoundException("Product not in wishlist");
        }

        wishlist.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return new()
        {
            Id = wishlist.Id,
            Productos = [..
                wishlist.Productos?.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Descripcion = p.Descripcion,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProducto = p.TipoProducto.Descripcion,
                    Imagen = p.Imagen,
                    Precio = p.Precio,
                    Activo = p.Activo
                }).ToList()
            ]
        };
    }
}
