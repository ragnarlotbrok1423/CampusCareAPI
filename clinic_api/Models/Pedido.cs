using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Pedido
{
    public int Idpedidos { get; set; }

    public int Cantidad { get; set; }

    public string Fecha { get; set; } = null!;

    public int MedicamentoFk { get; set; }

    public int FarmaceuticoFk { get; set; }

    public virtual Doctore FarmaceuticoFkNavigation { get; set; } = null!;

    public virtual Medicamento MedicamentoFkNavigation { get; set; } = null!;
}
