namespace campusCareAPI.Models
{
    public class PacientesDTO
    {
        public int IdUsuarios { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;
        public sbyte Visible { get; set; }


        public InformacionMedicaDTO InformacionMedica { get; set; }



    }
}
