using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<Usuario> Usuarios { get; set; } = new();
    }
}
