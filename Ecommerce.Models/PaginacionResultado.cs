using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class PaginacionResultado<T>
    {
        public int TotalPaginas { get; set; }
        public int PaginaActual { get; set; }
        public int ProductosTotales { get; set; }
        public List<T> Productos { get; set; } = [];
    }

}
