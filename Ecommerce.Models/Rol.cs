

namespace Ecommerce.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
