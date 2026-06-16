namespace AgendaPetAPI.DTOs.AgendamentoDTO
{
    public class LerAgendamentoDto
    {
        public Guid AgendamentoID { get; set; }
        public DateOnly DataAgendamento { get; set; }
        public TimeOnly HoraAgendamento { get; set; }
        public decimal ValorTotal { get; set; }
        public int TempoTotal { get; set; }

        public Guid StatusAgendamentoID { get; set; }
        public string NomeStatus { get; set; } = string.Empty;

        public Guid FuncionarioID { get; set; }
        public string NomeFuncionario { get; set; } = string.Empty;

        public Guid PetID { get; set; }
        public string NomePet { get; set; } = string.Empty;
        public string NomeRaca { get; set; } = string.Empty;
        public string NomePorte { get; set; } = string.Empty;

        public Guid TutorID { get; set; }
        public string NomeTutor { get; set; } = string.Empty;
        public string TelefoneTutor { get; set; } = string.Empty;

        public string ServicosPrestados { get; set; } = string.Empty;
    }
}
