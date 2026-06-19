using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Listar();
        public Usuario ObterPorId(Guid id);
        public List<Usuario> ObterPorNome(string nome);
        public Usuario ObterPorEmail(string email);
        public Usuario ObterPorTelefone(string telefone);
        public void Adicionar(Usuario usuario);
        public void Atualizar(Guid id, Usuario usuario);
        public void AtualizarSenha(string email, string senha);
        public void Remover(Guid id);
    }
}
