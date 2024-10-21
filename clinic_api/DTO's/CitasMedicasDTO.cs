using campusCareAPI.DTO_s;

namespace campusCareAPI.Models
{
    public class CitasMedicasDTO
    {
        public int IdcitasMedicas { get; set; }
        public string Fecha { get; set; }

        public TipoConsultaDTO TipoConsulta { get; set; }
        public PacientesDTO Usuarios { get; set; }
        public DoctoresDTO Doctores { get; set; }

    }
}
