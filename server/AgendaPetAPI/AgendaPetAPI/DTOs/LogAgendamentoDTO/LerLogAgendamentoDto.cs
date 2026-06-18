using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.LerLogAgendamentoDto
{
    public class LerAgendamentoDto
    {
        public Guid LogAgendamentoID { get; set; }

        public DateTime? DataModificacao { get; set; }

        public DateTime DataAnteriorAgendamento { get; set; }

        public string StatusAgendamentoAnterior { get; set; } = null!;

        public string ServicosPorAgendamento { get; set; } = null!;

        public Guid AgendamentoID { get; set; }
    }
}