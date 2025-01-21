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
            return await _context.Productos
                .Include(p => p.TipoProducto)
                .Select(p => new Producto
                {
                    Id = p.Id,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProducto = p.TipoProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Activo = p.Activo,
                    FechaAlta = p.FechaAlta,
                    Lotes = p.Lotes,
                    Wishlists = p.Wishlists
                })
                .ToListAsync();
        }

        public async Task<Producto> GetById(Guid id) 
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        private readonly int _records = 5;

        public async Task<PaginacionResultado<ProductoDto>> BuscarProductos(string? Tipo, int? Precio, int? page)
        {
            int _page = page ?? 1;

            var query = _context.Productos.Include(p => p.TipoProducto).AsQueryable();

            if(!string.IsNullOrEmpty(Tipo))
            {
                query = query.Where(p => p.TipoProducto.Descripcion.Contains(Tipo));
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
                                TipoProducto = p.TipoProducto.Descripcion,
                                Descripcion = p.Descripcion,
                                Precio = p.Precio,
                                Activo = true,
                                Lotes = p.Lotes
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

            producto.TipoProducto = productoActualizado.TipoProducto;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Wishlists = productoActualizado.Wishlists;
            await _context.SaveChangesAsync();

            return productoActualizado;
        }

        public async Task<bool> Delete(Guid id)
        {
            var producto = await GetById(id);

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
             return true;

        }
    }
}
