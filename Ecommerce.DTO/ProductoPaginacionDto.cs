using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class ProductoPaginacionDto
    {
        public Guid? Id { get; set; }
        public string? TipoProducto { get; set; }
        public string? Imagen { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public bool? Activo { get; set; }
        public List<LotePaginacionDto>? Lotes { get; set; } = [];
    }
}
