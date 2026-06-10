using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IComportamentoPetRepository
    {
        public List<ComportamentoPet> Listar();
        public ComportamentoPet ObterPorId(int id);
    }
}
