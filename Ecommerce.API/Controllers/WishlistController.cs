using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Interfaces;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController(IWishlistService service) : ControllerBase
    {
        readonly IWishlistService _wishlistService = service;

        [HttpGet]
        public async Task<IActionResult> Get(Guid ID)
        {
            var wishlists = await _wishlistService.GetWishlist(ID);
            return Ok(wishlists);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Guid ID)
        {
            var wishlist = await _wishlistService.CreateWishlist(ID);
            return Ok(wishlist);
        }

        [HttpPost("{idWishlist}/Productos")]
        public async Task<IActionResult> AddProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _wishlistService.AddProduct(idWishlist, idProducto);
            return Ok(wishlist);
        }

        [HttpDelete("{idWishlist}/Productos/{idProducto}")]
        public async Task<IActionResult> RemoveProduct(Guid idWishlist, Guid idProducto)
        {
            var wishlist = await _wishlistService.RemoveProduct(idWishlist, idProducto);
            return Ok(wishlist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid ID)
        {
            var result = await _wishlistService.DeleteWishlist(ID);
            return Ok(result);
        }

    }
}
