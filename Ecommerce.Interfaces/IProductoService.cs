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
        Task<IEnumerable<Producto>> GetAll();
        Task<Producto> GetById(Guid id);
        Task<PaginacionResultado<ProductoDto>> BuscarProductos(string? Titulo, int? Precio, int? page);
        Task<Producto> Create(Producto producto);
        Task<Producto> Update(Guid id, Producto productoActualizado);
        Task<bool> Delete(Guid id);
    }
}
