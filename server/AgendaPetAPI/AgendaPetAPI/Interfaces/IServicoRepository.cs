using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IServicoRepository
    {
        public List<Servico> Listar();
        public Servico ObterPorId(Guid id);
    }
}
