using campusCareAPI.DTO_s;
using clinic_api.DTO_s;
using clinic_api.Models;

namespace campusCareAPI.Models
{
    public class CitasMedicasDTO
    {
        public int IdcitasMedicas { get; set; }
        public string Fecha { get; set; }
        public string? Descripcion { get; set; }
        
        public EstadoDTO Estado { get; set; }
        public TipoConsultaDTO TipoConsulta { get; set; }
        public PacientesDTO Usuarios { get; set; }
        public DoctoresDTO Doctores { get; set; }

    }
}
