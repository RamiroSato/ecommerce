

namespace Ecommerce.Models
{
    public class Usuario
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Nombre { get; set; } 

        public string? Apellido { get; set; } 

        public string? Password { get; set; } 

        public string? Email { get; set; } 
        public string? Tipo { get; set; }

        public int IsActive { get; set; } = 1;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Usuario( string nombre, string apellido, string password, string email, string tipo)
        {            
            Nombre = nombre;
            Apellido = apellido;
            Password = password;
            Email = email;
            Tipo = tipo;
        }

        public Usuario() { }
    }
}
