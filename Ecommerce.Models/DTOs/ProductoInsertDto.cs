using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.DTOs
{
    public class ProductoInsertDto
    {
        public int IdTipoProducto { get; set; }
        public IFormFile Imagen { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
    }
}
