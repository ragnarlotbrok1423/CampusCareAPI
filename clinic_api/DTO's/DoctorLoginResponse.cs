namespace clinic_api.DTO_s
{
    public class DoctorLoginResponse
    {
        public int IdDoctores { get; set; }

        public string NombreCompleto { get; set; } = null!;
        public int EspecialidadFk { get; set; }
    }
}
