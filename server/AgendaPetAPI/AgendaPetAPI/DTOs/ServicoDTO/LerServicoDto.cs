namespace AgendaPetAPI.DTOs.ServicoDTO
{
    public class LerServicoDto
    {
        public Guid ServicoID { get; set; }

        public string NomeServico { get; set; } = null!;

        public decimal Preco { get; set; }

        public int TempoServico { get; set; }
    }
}
