using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Estado
{
    public int Idestados { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<CitasMedica> CitasMedicas { get; set; } = new List<CitasMedica>();
}
