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
        private readonly IS3Service _s3Service;

        public ProductoService(EcommerceContext context, IS3Service s3Service)
        {
            _context = context;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<ProductoDto>> GetAll()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    IdTipoProducto = p.IdTipoProducto,
                    TipoProducto = p.TipoProducto.Descripcion,
                    Imagen = p.Imagen,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Activo = p.Activo,
                    FechaAlta = p.FechaAlta,
                    Lotes = p.Lotes.Select(l => new LoteProductoDto
                    {
                        Id = l.Id,
                        Descripcion = l.Descripcion,
                        Cantidad = l.Cantidad,
                        Activo = l.Activo,
                        FechaAlta = l.FechaAlta
                    }).ToList(),
                    Wishlists = p.Wishlists
                })
                .ToListAsync();
        }

        public async Task<Producto> GetById(Guid id) 
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        private readonly int _records = 5;

        public async Task<PaginacionResultado<ProductoPaginacionDto>> BuscarProductos(string? Tipo, int? Precio, int? page)
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
                            .Select(p => new ProductoPaginacionDto
                            {
                                TipoProducto = p.TipoProducto.Descripcion,
                                Imagen = p.Imagen,
                                Descripcion = p.Descripcion,
                                Precio = p.Precio,
                                Activo = true,
                                Lotes = p.Lotes.Select(l => new LotePaginacionDto
                                {
                                    Descripcion = l.Descripcion,
                                    Cantidad = l.Cantidad,
                                    Activo = l.Activo,
                                }).ToList()
                            })
                            .ToListAsync();

            return new PaginacionResultado<ProductoPaginacionDto>
            {
                TotalPaginas = totalPages,
                PaginaActual = _page,
                ProductosTotales = totalRecords,
                Productos = productos
            };
        }

        public async Task<Producto> Create(ProductoInsertDto insertDto)
        {
            string urlImagen = await _s3Service.UploadFileAsync(insertDto.Imagen);

            Producto producto = new Producto()
            {
                IdTipoProducto = insertDto.IdTipoProducto,
                Imagen = urlImagen,
                Descripcion = insertDto.Descripcion,
                Precio = insertDto.Precio,
                Activo = insertDto.Activo
            };

            if (producto.Id == Guid.Empty)
            {
                producto.Id = Guid.NewGuid();
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<ProductoUpdateDto> Update(Guid id, ProductoUpdateDto productoActualizado)
        {
            var producto = await _context.Productos.Include(p => p.TipoProducto).FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return null;
                throw new ArgumentException("No se encontró el producto declarado");
            }

            producto.TipoProducto.Descripcion = productoActualizado.TipoProducto;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Activo = productoActualizado.Activo;

            await _context.SaveChangesAsync();

            return new ProductoUpdateDto
            {
                TipoProducto = producto.TipoProducto.Descripcion,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Activo = producto.Activo,
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
             return true;

        }
    }
}
