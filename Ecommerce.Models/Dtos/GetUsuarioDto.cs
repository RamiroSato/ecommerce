﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.Dtos
{
    public class GetUsuarioDto
    {

       

        public string Nombre { get; set; }

        public string Apellido { get; set; }
        
        public string Email { get; set; }
        
        public bool IsActive { get; set; }

    }
}
