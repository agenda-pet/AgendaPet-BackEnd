using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IStatusUsuarioRepository
    {
        public List<StatusUsuario> Listar();
        public StatusUsuario ObterPorId(int id);
    }
}
