using campusCareAPI.DTO_s;

namespace clinic_api.DTO_s
{
    public class PedidosDTO
    {
        public int Idpedidos { get; set; }

        public int Cantidad { get; set; }

        public string Fecha { get; set; } = null!;
        public MedicamentosDTO Medicamentos { get; set; } = null!;
        public DoctoresDTO Doctores { get; set; } = null!;
    }
    public class createPedidoDTO
    {
        public int Cantidad { get; set; }
        public string Fecha { get; set; } = null!;
        public int MedicamentoFk { get; set; }
        public int FarmaceuticoFk { get; set; }
    }
}
