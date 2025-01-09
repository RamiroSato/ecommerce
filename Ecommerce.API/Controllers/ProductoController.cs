using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Contexts;
using Ecommerce.Models;
using Ecommerce.Interfaces;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(Guid id)
        {
            var producto = await _productoService.GetAsyncById(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(Guid id, Producto productoActualizado)
        {
            if (id != productoActualizado.Id)
            {
                return BadRequest("No es posible realizar la operación: el id proporcionado no coincide con ningún producto en la base de datos");
            }

            var producto = await _productoService.GetAsyncById(id);

            if (producto == null)
            {
                return NotFound("No se encontró el producto"); 
            }

            var actualizar = await _productoService.UpdateAsync(id, productoActualizado);

            if(actualizar == null)
            {
                return NotFound("No se pudo actualizar el producto");
            }

            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            var productoCreado = await _productoService.CreateAsync(producto);

            return CreatedAtAction("GetProducto", new { id = productoCreado.Id }, productoCreado);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            var producto = await _productoService.GetAsyncById(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            await _productoService.DeleteAsync(id);

            return NoContent();
        }
    }
}
