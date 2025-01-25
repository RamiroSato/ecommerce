using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Producto
    {
        public Guid Id { get; set; }
        public int IdTipoProducto { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Lote> Lotes { get; set; }
        public List<Wishlist>? Wishlists { get; set; }
    }
}
