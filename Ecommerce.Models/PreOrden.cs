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
    public Guid IdUsuario { get; set; }
    public string Descripcion { get; set; }
    public bool Activo { get; set; }
    public DateTime Vencimiento { get; set; }
    public DateTime FechaAlta { get; set; }
    public List<ItemPreOrden> Items { get; set; }
}
