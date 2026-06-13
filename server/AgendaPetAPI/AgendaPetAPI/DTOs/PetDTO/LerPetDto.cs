using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.PetDTO
{
    public class LerPetDto
    {
        public Guid PetID { get; set; }

        public string Nome { get; set; } = null!;

        public Guid TipoAnimalID { get; set; }
        public string TipoAnimal { get; set; }

        public Guid ComportamentoID { get; set; }
        public string Comportamento { get; set; }

        public Guid RacaID { get; set; }
        public string Raca { get; set; }

        public Guid PorteID { get; set; }
        public string Porte { get; set; }
        public string NomeDono { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}
