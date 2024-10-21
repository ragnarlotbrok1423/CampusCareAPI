using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Paciente
{
    public int IdPacientes { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public int InformacionMedica { get; set; }

    public sbyte Visibiliy { get; set; }

    public virtual ICollection<CitasMedica> CitasMedicas { get; set; } = new List<CitasMedica>();

    public virtual InformacionesMedica InformacionMedicaNavigation { get; set; } = null!;

    public virtual ICollection<RegistroDeEntrega> RegistroDeEntregas { get; set; } = new List<RegistroDeEntrega>();

    public virtual ICollection<Usuariosxhistorialmedico> Usuariosxhistorialmedicos { get; set; } = new List<Usuariosxhistorialmedico>();
}
