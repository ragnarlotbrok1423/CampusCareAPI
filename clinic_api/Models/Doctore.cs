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

    public virtual ICollection<Certificado> Certificados { get; set; } = new List<Certificado>();

    public virtual ICollection<CitasMedica> CitasMedicas { get; set; } = new List<CitasMedica>();

    public virtual ICollection<DonantesSangre> DonantesSangres { get; set; } = new List<DonantesSangre>();

    public virtual Especialidade EspecialidadFkNavigation { get; set; } = null!;

    public virtual InformacionesMedica InformacionMedicaNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();

    public virtual ICollection<Referencia> Referencia { get; set; } = new List<Referencia>();
}
