namespace campusCareAPI.Models
{
    public class CreatePacienteDTO
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;

        public string? Alergia { get; set; }

        public string NumeroSecundario { get; set; } = null!;

        public int Tipaje { get; set; }

        public sbyte Visible { get; set; }
    }
}
