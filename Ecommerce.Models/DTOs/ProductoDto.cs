using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.DTOs
{
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<LoteProductoDto> Lotes { get; set; } = new();
        public List<Wishlist>? Wishlists { get; set; }
    }
}
