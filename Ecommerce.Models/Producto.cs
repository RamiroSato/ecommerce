using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Producto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public Guid WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }
    }
}
