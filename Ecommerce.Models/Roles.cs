

namespace Ecommerce.Models
{
    public class Roles
    {
        public int Id { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        //public DateTime FechaAlta { get; set; }
    }
}
