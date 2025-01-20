

namespace Ecommerce.Models
{
    public class Usuario
    {

        public Guid Id { get; set; } 

        public string Nombre { get; set; } 

        public string Apellido { get; set; } 

        public string Password { get; set; } 

        public string Email { get; set; } 
        public string Tipo { get; set; }

        public int IsActive { get; set; } 

        

        
    }
}
