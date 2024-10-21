using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Medicamento
{
    public int Idmedicamento { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
