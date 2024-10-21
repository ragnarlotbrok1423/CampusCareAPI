using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class HistoriaClinica
{
    public int IdhistoriaClinica { get; set; }

    public float Peso { get; set; }

    public float Temperatura { get; set; }

    public float Glisemia { get; set; }

    public float Presion { get; set; }

    public virtual ICollection<Usuariosxhistorialmedico> Usuariosxhistorialmedicos { get; set; } = new List<Usuariosxhistorialmedico>();
}
