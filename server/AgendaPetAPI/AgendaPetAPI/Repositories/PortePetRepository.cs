using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class PortePetRepository : IPortePetRepository
    {
        private readonly AgendaPetDbContext _context;
        public PortePetRepository(AgendaPetDbContext context) => _context = context;

        public List<PortePet> Listar()
        {
            return _context.PortePet.OrderBy(p => p.NomePorte).ToList();
        }

        public PortePet ObterPorId(Guid id)
        {
            return _context.PortePet.Find(id);
        }
    }
}
