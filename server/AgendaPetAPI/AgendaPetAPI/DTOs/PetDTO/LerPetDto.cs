using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.PetDTO
{
    public class LerPetDto
    {
        public Guid PetID { get; set; }

        public string Nome { get; set; } = null!;

        public Guid TipoAnimalID { get; set; }

        public string nomeTipo { get; set; }

        public Guid ComportamentoID { get; set; }

        public string nomeComportamento{ get; set; }

        public Guid RacaID { get; set; }

        public string nomeRaca{ get; set; }

        public Guid PorteID { get; set; }

        public string nomePorte{ get; set; }

        public string NomeDono { get; set; }
        
        public Guid UsuarioID { get; set; }

        public List<AgendamentoPorPetDto> Agendamentos { get; set; } = new List<AgendamentoPorPetDto>();
    }
}
