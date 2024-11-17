using campusCareAPI.DTO_s;
using campusCareAPI.Models;

namespace clinic_api.DTO_s
{
    public class ReferenciasDTO
    {

        public int Idreferencias { get; set; }

        public string Fecha { get; set; } = null!;

        public string CondicionMedica { get; set; } = null!;

        public string Sintomas { get; set; } = null!;

        public string Diagnostico { get; set; } = null!;

        public string Especialidad { get; set; } = null!;

        public string Pdf { get; set; } = null!;
        public DoctoresDTO Doctor { get; set; } = null!;
        public PacientesDTO Paciente { get; set; } = null!;

    }
}
