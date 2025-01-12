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
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public List<Wishlist>? Wishlists { get; set; }
    }

    public class ProductoDto
    {
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
    }

}
