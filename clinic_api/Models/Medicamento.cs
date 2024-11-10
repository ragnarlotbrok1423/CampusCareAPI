using System;
using System.Collections.Generic;

namespace clinic_api.Models;

public partial class Medicamento
{
    public int Idmedicamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int CategoriaFk { get; set; }

    public int CantidadStock { get; set; }

    public virtual Categoria CategoriaFkNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
