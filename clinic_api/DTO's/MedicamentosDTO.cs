namespace clinic_api.DTO_s
{
    public class MedicamentosDTO
    {
        public int Idmedicamento { get; set; }

        public string Nombre { get; set; } = null!;

        public CategoriaDTO Categoria { get; set; } = null!;

        public int CantidadStock { get; set; }

    }

    public class AddMedicamentoDTO
    {
        public string Nombre { get; set; } = null!;

        public int CategoriaFk { get; set; }

        public int CantidadStock { get; set; }
    }
}
