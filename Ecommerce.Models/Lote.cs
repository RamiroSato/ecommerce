using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Lote
    {
        public Guid Id { get; set; }
        public Guid IdProducto { get; set; }
        public Producto Producto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
