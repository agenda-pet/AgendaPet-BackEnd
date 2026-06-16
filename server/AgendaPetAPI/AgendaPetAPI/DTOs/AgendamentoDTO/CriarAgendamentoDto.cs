namespace AgendaPetAPI.DTOs.AgendamentoDTO
{
    public class CriarAgendamentoDto
    {
        public DateOnly DataAgendamento { get; set; }
        public TimeOnly HoraAgendamento { get; set; }
        public Guid StatusAgendamentoID { get; set; }
        public Guid FuncionarioID { get; set; }
        public Guid PetID { get; set; }

        public List<Guid> ServicosIds { get; set; } = new List<Guid>();
    }
}
