using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> GetAsyncById(Guid id);
        Task<Producto> CreateAsync(Producto producto);
        Task<Producto> UpdateAsync(Guid id, Producto productoActualizado);
        Task DeleteAsync(Guid id);
    }
}
