using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Usuariosxhistorialmedico
{
    public int IdusuariosXhistorialMedico { get; set; }

    public int Pacientes { get; set; }

    public int HistorialMedico { get; set; }

    public virtual HistoriaClinica HistorialMedicoNavigation { get; set; } = null!;

    public virtual Paciente PacientesNavigation { get; set; } = null!;
}
