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

namespace Ecommerce.Services
{
    public class WishlistService(EcommerceContext context) : IWishlistService
    {
        readonly EcommerceContext _context = context;

        public async Task<Wishlist> AddProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _context.Wishlists.FindAsync(idWishlist) ?? throw new ResourceNotFoundException("Wishlist not found");
            var producto = await _context.Productos.FindAsync(idProducto) ?? throw new ResourceNotFoundException("Product not found");

            if(!wishlist.Usuario.Activo)
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
            return wishlist;

        }

        public async Task<Wishlist> CreateWishlist(Guid idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario) ?? throw new ResourceNotFoundException("User not found");
            if (!usuario.Activo)
            {
                throw new ResourceNotFoundException("User not available");
            }
            
            if (usuario.Wishlist != null)
            {
                throw new ResourceNotFoundException("User already has a wishlist");
            }

            var wishlist = new Wishlist
            {
                IdUsuario = idUsuario,
                Usuario = usuario,
                Productos = []
            };

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<bool> DeleteWishlist(Guid id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id) ?? throw new ResourceNotFoundException("Wishlist not found");
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Wishlist> GetWishlist(Guid id)
        {
            return await _context.Wishlists.FindAsync(id) ?? throw new ResourceNotFoundException("Wishlist not found");
        }

        public async Task<Wishlist> RemoveProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _context.Wishlists.FindAsync(idWishlist) ?? throw new ResourceNotFoundException("Wishlist not found");
            var producto = await _context.Productos.FindAsync(idProducto) ?? throw new ResourceNotFoundException("Product not found");
            if (!wishlist.Productos.Any(p => p.Id == idProducto))
            {
                throw new ResourceNotFoundException("Product not in wishlist");
            }
            wishlist.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return wishlist;
        }
    }
}
