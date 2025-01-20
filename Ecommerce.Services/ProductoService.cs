using Ecommerce.Data.Contexts;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.Models.DTOs;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetById(Guid id) 
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        private readonly int _records = 5;

        public async Task<PaginacionResultado<ProductoDto>> BuscarProductos(string? Titulo, int? Precio, int? page)
        {
            int _page = page ?? 1;

            var query = _context.Productos.AsQueryable();

            if(!string.IsNullOrEmpty(Titulo))
            {
                query = query.Where(p => p.Titulo.Contains(Titulo));
            }

            if(Precio.HasValue)
            {
                query = query.Where(p => p.Precio == Precio);
            }

            int totalRecords = await query.CountAsync();

            int totalPages = (int)Math.Ceiling(totalRecords / (decimal)_records);

            var productos = await query
                            .Skip((_page - 1) * _records)
                            .Take(_records)
                            .Select(p => new ProductoDto
                            {
                                Titulo = p.Titulo,
                                Categoria = p.Categoria,
                                Descripcion = p.Descripcion,
                                Precio = p.Precio
                            })
                            .ToListAsync();

            return new PaginacionResultado<ProductoDto>
            {
                TotalPaginas = totalPages,
                PaginaActual = _page,
                ProductosTotales = totalRecords,
                Productos = productos
            };
        }

        public async Task<Producto> Create(Producto producto)
        {
            if (producto.Id == Guid.Empty)
            {
                producto.Id = Guid.NewGuid();
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<Producto> Update(Guid id, Producto productoActualizado)
        {
            var producto = await GetById(id);

            if (producto == null)
            {
                return null;
                throw new ArgumentException("No se encontró el producto declarado");
            }

            producto.Titulo = productoActualizado.Titulo;
            producto.Categoria = productoActualizado.Categoria;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Wishlists = productoActualizado.Wishlists;
            await _context.SaveChangesAsync();

            return productoActualizado;
        }

        public async Task Delete(Guid id)
        {
            var producto = await GetById(id);

            if(producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChangesAsync();
            }
        }
    }
}
