using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? CantidadDisponible { get; set; }

    public int? Medicamentofk { get; set; }

    public virtual Medicamento? MedicamentofkNavigation { get; set; }

    public virtual ICollection<RegistroDeEntrega> RegistroDeEntregas { get; set; } = new List<RegistroDeEntrega>();
}
