using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
