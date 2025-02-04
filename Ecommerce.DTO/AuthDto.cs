using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class AuthDto
    {        
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
    }
}
