namespace AgendaPetAPI.DTOs.UsuarioDTO
{
    public class CriarUsuarioDto
    {
        public string Nome { get; set; } = null!;

        public string NumeroTelefone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; }
        public bool? StatusUsuarioID { get; set; }

        public Guid TipoUsuarioID { get; set; }
    }
}
