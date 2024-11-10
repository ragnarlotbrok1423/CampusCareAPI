using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Receta
{
    public int IdRegistroDeEntrega { get; set; }

    public string FechaDeEntrega { get; set; } = null!;

    public int CantidadDeEntrega { get; set; }

    public string Observaciones { get; set; } = null!;

    public int PacienteFkk { get; set; }

    public int FarmaceuticoFkk { get; set; }

    public int MedicamentoFk { get; set; }

    public virtual Doctore FarmaceuticoFkkNavigation { get; set; } = null!;

    public virtual Medicamento MedicamentoFkNavigation { get; set; } = null!;

    public virtual Paciente PacienteFkkNavigation { get; set; } = null!;
}
