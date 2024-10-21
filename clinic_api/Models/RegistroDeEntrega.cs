using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class RegistroDeEntrega
{
    public int IdRegistroDeEntrega { get; set; }

    public string? FechaDeEntrega { get; set; }

    public int? CantidadDeEntrega { get; set; }

    public string? Observaciones { get; set; }

    public int? MedicamentoFkk { get; set; }

    public int? PacienteFkk { get; set; }

    public int? FarmaceuticoFkk { get; set; }

    public virtual Doctore? FarmaceuticoFkkNavigation { get; set; }

    public virtual Inventario? MedicamentoFkkNavigation { get; set; }

    public virtual Paciente? PacienteFkkNavigation { get; set; }
}
