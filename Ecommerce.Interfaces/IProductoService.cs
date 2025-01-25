using Ecommerce.Models;
using Ecommerce.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetAll();
        Task<Producto> GetById(Guid id);
        Task<PaginacionResultado<ProductoPaginacionDto>> BuscarProductos(string? Titulo, int? Precio, int? page);
        Task<Producto> Create(ProductoInsertDto insertDto);
        Task<ProductoUpdateDto> Update(Guid id, ProductoUpdateDto productoActualizado);
        Task<bool> Delete(Guid id);
    }
}
