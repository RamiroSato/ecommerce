using Ecommerce.Models;
using Ecommerce.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Interfaces
{
    public interface ILoteService
    {
        Task<IEnumerable<Lote>> GetLotesPorProducto(Guid idProducto);
        Task<Lote> GetLoteById(Guid id);
        Task<LoteDto> Create(LoteDto lote);
        Task<Lote> Update(Guid id, int cantidad, bool activo);
        Task<bool> Delete(Guid id);
    }
}
