using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public List<Producto>? Productos { get; set; }
    }
}
