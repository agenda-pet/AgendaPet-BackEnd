using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.PetDTO
{
    public class LerPetDto
    {
        public Guid PetID { get; set; }

        public string Nome { get; set; } = null!;

        public Guid TipoAnimalID { get; set; }

        public Guid ComportamentoID { get; set; }

        public Guid RacaID { get; set; }

        public Guid PorteID { get; set; }

        public Guid UsuarioID { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}
