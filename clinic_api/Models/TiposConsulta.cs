using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class TiposConsulta
{
    public int IdtiposConsultas { get; set; }

    public string TipoConsulta { get; set; } = null!;

    public virtual ICollection<CitasMedica> CitasMedicas { get; set; } = new List<CitasMedica>();
}
