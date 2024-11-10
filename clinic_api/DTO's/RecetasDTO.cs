using campusCareAPI.DTO_s;
using campusCareAPI.Models;

namespace clinic_api.DTO_s
{
    public class RecetasDTO
    {
        public int IdRegistroDeEntrega { get; set; }
        public string FechaDeEntrega { get; set; } = null!;
        public int CantidadDeEntrega { get; set; }
        public string Observaciones { get; set; } = null!;
        public PacientesDTO Paciente { get; set; } = null!;
        public DoctoresDTO Doctor { get; set; } = null!;
        public MedicamentosDTO Medicamento { get; set; } = null!;
    }
}
