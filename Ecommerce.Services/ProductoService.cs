using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ProductoService : IProductoService
    {
        private readonly EcommerceContext _context;

        public ProductoService(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetAsyncById(Guid id) 
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            if (producto.Id == Guid.Empty)
            {
                producto.Id = Guid.NewGuid();
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<Producto> UpdateAsync(Guid id, Producto productoActualizado)
        {
            var producto = await GetAsyncById(id);

            if (producto == null)
            {
                return null;
                throw new ArgumentException("No se encontró el producto declarado");
            }

            producto.Titulo = productoActualizado.Titulo;
            producto.Categoria = productoActualizado.Categoria;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Wishlist = productoActualizado.Wishlist;
            await _context.SaveChangesAsync();

            return productoActualizado;
        }

        public async Task DeleteAsync(Guid id)
        {
            var producto = await GetAsyncById(id);

            if(producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChangesAsync();
            }
        }
    }
}
