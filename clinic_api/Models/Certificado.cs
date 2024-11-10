using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Certificado
{
    public int Idcertificados { get; set; }

    public string Fecha { get; set; } = null!;

    public string Motivo { get; set; } = null!;

    public int PacienteFk { get; set; }

    public int DoctorFk { get; set; }

    public string Pdf { get; set; } = null!;

    public virtual Doctore DoctorFkNavigation { get; set; } = null!;

    public virtual Paciente PacienteFkNavigation { get; set; } = null!;
}
