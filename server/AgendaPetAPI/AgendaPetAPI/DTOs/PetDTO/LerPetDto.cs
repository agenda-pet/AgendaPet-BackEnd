using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.PetDTO
{
    public class LerPetDto
    {
        public Guid PetID { get; set; }

        public string Nome { get; set; } = null!;

        public string nomeTipo { get; set; }

        public string nomeComportamento{ get; set; }
        public string nomeRaca{ get; set; }

        public string nomePorte{ get; set; }
        public string NomeDono { get; set; }

        public List<AgendamentoPorPetDto> Agendamentos { get; set; } = new List<AgendamentoPorPetDto>();
    }
}
