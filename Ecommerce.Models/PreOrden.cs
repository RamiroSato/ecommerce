using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    IdUsuario: GUID
    Descripcion: string
    Activo: bool
    Vencimiento: DateTime
    FechaAlta: DateTime
*/

namespace Ecommerce.Models;

public class PreOrden
{
    public Guid Id { get; set; }
    public Guid IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
    public string Descripcion { get; set; }
    public DateTime Vencimiento { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaAlta { get; set; }
    public List<ItemPreOrden> Items { get; set; }
    public Orden Orden { get; set; }
    public Transaccion Transaccion { get; set; }
}
