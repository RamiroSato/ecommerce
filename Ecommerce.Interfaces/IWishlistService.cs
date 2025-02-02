using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces
{
    public interface IWishlistService
    {
        Task<Wishlist> GetWishlist(Guid id, string? requestCognitoId);
        Task<Wishlist> CreateWishlist(Guid idUsuario, string? requestCognitoId);
        Task<Wishlist> AddProduct(Guid idWishlist, Guid idProducto, string? RequestCognitoId);
        Task<Wishlist> RemoveProduct(Guid idWishlist, Guid idProducto, string? requestCognitoId);
        Task<bool> DeleteWishlist(Guid id, string? requestCognitoId);
    }
}
