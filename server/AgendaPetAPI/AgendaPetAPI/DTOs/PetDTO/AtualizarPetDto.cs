using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.PetDTO
{
    public class AtualizarPetDto
    {
        public string Nome { get; set; } = null!;

        public Guid ComportamentoID { get; set; }

        public Guid PorteID { get; set; }

        public Guid UsuarioID { get; set; }
    }
}
