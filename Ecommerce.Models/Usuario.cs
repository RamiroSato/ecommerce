﻿

namespace Ecommerce.Models
{
    public class Usuario
    {

        public Guid Id { get; set; } 

        public int IdRol { get; set; }

        public Roles Rol { get; set; }

        public string Nombre { get; set; } 

        public string Apellido { get; set; } 

        public string Password { get; set; } 

        public string Email { get; set; } 
        
        public bool IsActive { get; set; } 

        //public DateTime FechaAlta { get; set; }//Esta propiedad genera problemas a la hora de crear la base de datos

        

        
    }
}
