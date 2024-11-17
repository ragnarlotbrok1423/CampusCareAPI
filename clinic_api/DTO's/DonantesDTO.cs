using campusCareAPI.DTO_s;
using campusCareAPI.Models;
using System.Security.Policy;

namespace clinic_api.DTO_s
{
    public class DonantesDTO
    {
        public int IddonantesSangre { get; set; }

        public string Fecha { get; set; } = null!;



        public PacientesDTO paciente { get; set; } = null!;
        public DoctoresDTO doctores { get; set; } = null!;
    }
}
