using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class CitasMedica
{
    public int IdcitasMedicas { get; set; }

    public string Fecha { get; set; } = null!;

    public int TipoConsulta { get; set; }

    public int Paciente { get; set; }

    public int Doctor { get; set; }

    public sbyte Visible { get; set; }

    public int Estado { get; set; }

    public virtual Doctore DoctorNavigation { get; set; } = null!;

    public virtual Estado EstadoNavigation { get; set; } = null!;

    public virtual Paciente PacienteNavigation { get; set; } = null!;

    public virtual TiposConsulta TipoConsultaNavigation { get; set; } = null!;
}
