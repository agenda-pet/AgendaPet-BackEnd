using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IPortePetRepository
    {
        public List<PortePet> Listar();

        public PortePet ObterPorId(int id);
    }
}
