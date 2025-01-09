﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Usuario
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty ;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        
    }
}
