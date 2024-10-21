using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Doctore
{
    public int IdDoctores { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int EspecialidadFk { get; set; }

    public string Diploma { get; set; } = null!;

    public string Perfil { get; set; } = null!;

    public int InformacionMedica { get; set; }

    public virtual ICollection<CitasMedica> CitasMedicas { get; set; } = new List<CitasMedica>();

    public virtual Especialidade EspecialidadFkNavigation { get; set; } = null!;

    public virtual InformacionesMedica InformacionMedicaNavigation { get; set; } = null!;

    public virtual ICollection<RegistroDeEntrega> RegistroDeEntregas { get; set; } = new List<RegistroDeEntrega>();
}
