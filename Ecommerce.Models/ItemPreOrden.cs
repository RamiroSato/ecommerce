using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
ID: GUID
IdLote: GUID
IdPreOrden: GUID
PrecioUnitario: decimal
Cantidad: Int
Activo: bool
FechaAlta: DateTime
*/

namespace Ecommerce.Models;

public class ItemPreOrden
{
    public Guid ID { get; set; }
    public Guid IdLote { get; set; }
    public Guid IdPreOrden { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int Cantidad { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaAlta { get; set; }
}
