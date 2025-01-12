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
        public List<Producto>? Productos { get; set; }
    }
}
