namespace clinic_api.DTO_s
{
    public class CreateReceta
    {
        public string FechaDeEntrega { get; set; } = null!;
        public int CantidadDeEntrega { get; set; }
        public string Observaciones { get; set; } = null!;
        public int IdPaciente { get; set; }
        public int IdDoctor { get; set; }
        public int IdMedicamento { get; set; }
    }
}
