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
using Ecommerce.DTO;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController(IProductoService productoService) : ControllerBase
    {
        private readonly IProductoService _productoService = productoService;

        // SEARCH: api/BuscarProducto
        [HttpGet]
        public async Task<ActionResult<PaginacionResultado<ProductoUpdateDto>>> BuscarProductos(
            [FromQuery] string? Tipo,
            [FromQuery] int? Precio,
            [FromQuery] int? Page)
        {
            var resultado = await _productoService.BuscarProductos(Tipo, Precio, Page);

            return Ok(resultado);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(Guid id)
        {
            var producto = await _productoService.GetById(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(Guid id, ProductoUpdateDto productoActualizado)
        {
            var actualizado = await _productoService.Update(id, productoActualizado);

            if(actualizado == null)
            {
                return NotFound("No se pudo actualizar el producto");
            }

            return Ok(actualizado);
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(ProductoInsertDto insertDto)
        {
            var productoCreado = await _productoService.Create(insertDto);
            return CreatedAtAction("GetProducto", new { id = productoCreado.Id }, productoCreado);
        }

        // DELETE: api/Producto/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            var producto = await _productoService.GetById(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            await _productoService.Delete(id);

            return Ok($"Se borró el producto con id {id} exitosamente");
        }
    }
}
