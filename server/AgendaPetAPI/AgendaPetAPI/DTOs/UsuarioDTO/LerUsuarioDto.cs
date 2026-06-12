using AgendaPetAPI.Domains;

namespace AgendaPetAPI.DTOs.UsuarioDTO
{
    public class LerUsuarioDto
    {
        public Guid UsuarioID { get; set; }

        public string Nome { get; set; } = null!;

        public string NumeroTelefone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Guid TipoUsuarioID { get; set; }

        public bool? StatusUsuarioID { get; set; }

        public List<string> NomePet { get; set; }
    }
}
