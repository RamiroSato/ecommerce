using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
ID: GUID
IdPreOrden: GUID
FechaPago: int
Monto: decimal
NombreTarjeta: string
NumeroTarjeta: BigInt
Activo: bool
FechaAlta: DateTime
 */

namespace Ecommerce.Models;
public class Transaccion
{
    public Guid Id { get; set; }
    public Guid IdPreOrden { get; set; }
    public PreOrden PreOrden { get; set; }
    public int FechaPago { get; set; }
    public decimal Monto { get; set; }
    public string NombreTarjeta { get; set; }
    public long NumeroTarjeta { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaAlta { get; set; }
}
