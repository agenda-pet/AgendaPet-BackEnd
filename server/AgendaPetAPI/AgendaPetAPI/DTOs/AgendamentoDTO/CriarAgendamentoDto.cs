using System.ComponentModel.DataAnnotations;

namespace AgendaPetAPI.DTOs.AgendamentoDTO
{
    public class CriarAgendamentoDto
    {
        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateOnly DataAgendamento { get; set; }

        [Required(ErrorMessage = "O horário do agendamento é obrigatório.")]
        public TimeOnly HoraAgendamento { get; set; }

        [Required(ErrorMessage = "O ID do status é obrigatório.")]
        public Guid StatusAgendamentoID { get; set; }

        [Required(ErrorMessage = "O ID do pet é obrigatório.")]
        public Guid PetID { get; set; }

        public List<Guid> ServicosIds { get; set; } = new List<Guid>();
    }
}
