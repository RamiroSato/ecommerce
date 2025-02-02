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

namespace Ecommerce.Services
{
    public class WishlistService(EcommerceContext context) : IWishlistService
    {
        readonly EcommerceContext _context = context;

        private async Task<bool> CheckCognitoId (string? requestCognitoId, string wishlistCognitoId)
        {
            var usuarioCognito = await _context.Usuarios.FirstOrDefaultAsync(u => u.CognitoId == requestCognitoId) ?? throw new UnauthorizedAccessException("You are not authorized to modify this user.");

            if (wishlistCognitoId != requestCognitoId && usuarioCognito.IdRol != 1)
                throw new UnauthorizedAccessException("You are not authorized to modify this user.");

            return true;
        }


        public async Task<Wishlist> AddProduct(Guid idWishlist, Guid idProducto, string? requestCognitoId)
        {
            var wishlist = await _context.Wishlists.FindAsync(idWishlist) ?? throw new ResourceNotFoundException("Wishlist not found");
            var producto = await _context.Productos.FindAsync(idProducto) ?? throw new ResourceNotFoundException("Product not found");

            await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

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

        public async Task<Wishlist> CreateWishlist(Guid idUsuario, string? requestCognitoId)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario) ?? throw new ResourceNotFoundException("User not found");

            await CheckCognitoId(requestCognitoId, usuario.CognitoId);

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

        public async Task<bool> DeleteWishlist(Guid id, string? requestCognitoId)
        {
            var wishlist = await _context.Wishlists.FindAsync(id) ?? throw new ResourceNotFoundException("Wishlist not found");

            await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId); 

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Wishlist> GetWishlist(Guid id, string? requestCognitoId)
        {
            var wishlist = await _context.Wishlists.FindAsync(id) ?? throw new ResourceNotFoundException("Wishlist not found");
            
            await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

            return wishlist;
        }

        public async Task<Wishlist> RemoveProduct(Guid idWishlist, Guid idProducto, string? requestCognitoId)
        {
            var wishlist = await _context.Wishlists.FindAsync(idWishlist) ?? throw new ResourceNotFoundException("Wishlist not found");
            var producto = await _context.Productos.FindAsync(idProducto) ?? throw new ResourceNotFoundException("Product not found");

            await CheckCognitoId(requestCognitoId, wishlist.Usuario.CognitoId);

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
