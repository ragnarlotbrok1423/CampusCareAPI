using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Especialidade
{
    public int IdEspecialidades { get; set; }

    public string Especialidad { get; set; } = null!;

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();
}
