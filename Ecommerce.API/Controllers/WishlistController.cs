using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Interfaces;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Cliente")]
    [ApiController]
    public class WishlistController(IWishlistService service) : ControllerBase
    {
        readonly IWishlistService _wishlistService = service;

        [HttpGet]
        public async Task<IActionResult> Get(Guid ID)
        {
            var wishlists = await _wishlistService.GetWishlist(ID, HttpContext.Items["cognitoId"]?.ToString());
            return Ok(wishlists);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid IdUsuario)
        {
            var wishlist = await _wishlistService.CreateWishlist(IdUsuario, HttpContext.Items["cognitoId"]?.ToString());
            return Ok(wishlist);
        }

        [HttpPost("{idWishlist}/Productos")]
        public async Task<IActionResult> AddProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _wishlistService.AddProduct(idWishlist, idProducto, HttpContext.Items["cognitoId"]?.ToString());
            return Ok(wishlist);
        }

        [HttpDelete("{idWishlist}/Productos/{idProducto}")]
        public async Task<IActionResult> RemoveProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _wishlistService.RemoveProduct(idWishlist, idProducto, HttpContext.Items["cognitoId"]?.ToString());
            return Ok(wishlist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid ID)
        {
            var result = await _wishlistService.DeleteWishlist(ID, HttpContext.Items["cognitoId"]?.ToString());
            return Ok(result);
        }

    }
}
