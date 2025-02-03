using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class WishlistDTO
    {
        public Guid? Id { get; set; }
        public Guid? IdUsuario { get; set; }
        public UsuarioGetDto? Usuario { get; set; }
        public List<ProductoDto>? Productos { get; set; }
    }
}
