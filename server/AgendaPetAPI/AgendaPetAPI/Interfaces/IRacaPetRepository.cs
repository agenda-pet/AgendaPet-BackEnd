using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IRacaPetRepository
    {
        public List<RacaPet> Listar();
        public RacaPet ObterPorId(Guid id);
    }
}
