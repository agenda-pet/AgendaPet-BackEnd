namespace AgendaPetAPI.DTOs.UsuarioDTO
{
    public class AtualizarUsuarioDto
    {
        public string Nome { get; set; } = null!;

        public string NumeroTelefone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Guid TipoUsuarioID { get; set; }

    }
}
