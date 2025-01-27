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
        Task<Wishlist> GetWishlist(Guid id);
        Task<Wishlist> CreateWishlist(Guid idUsuario);
        Task<Wishlist> AddProduct(Guid idWishlist, Guid idProducto);
        Task<Wishlist> RemoveProduct(Guid idWishlist, Guid idProducto);
        Task<bool> DeleteWishlist(Guid id);
    }
}
