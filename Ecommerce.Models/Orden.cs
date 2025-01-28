using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models;
public class Orden
{
    public Guid Id { get; set; }
    public Guid IdPreOrden { get; set; }
    public PreOrden PreOrden { get; set; }
    public int NumeroOrden { get; set; }
    public DateTime FechaEntrega { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaAlta { get; set; }
}