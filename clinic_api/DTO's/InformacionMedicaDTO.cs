namespace campusCareAPI.Models
{
    public class InformacionMedicaDTO
    {
        public string? Alergia { get; set; }

        public string NumeroSecundario { get; set; } = null!;

        public TipajesSanguineosDTO TipajeSanguineo { get; set; } = null!;
    }
}
