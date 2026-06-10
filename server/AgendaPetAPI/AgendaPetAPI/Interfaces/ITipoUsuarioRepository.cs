using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        public List<TipoUsuario> Listar();
        public TipoUsuario ObterPorId(int id);

    }
}
