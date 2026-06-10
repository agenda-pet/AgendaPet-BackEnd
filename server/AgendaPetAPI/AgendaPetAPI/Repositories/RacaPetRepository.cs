using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class RacaPetRepository : IRacaPetRepository
    {
        private readonly AgendaPetDbContext _context;
        public RacaPetRepository(AgendaPetDbContext context) => _context = context;

        public List<RacaPet> Listar()
        {
            return _context.RacaPet.OrderBy(p => p.NomeRaca).ToList();
        }

        public RacaPet ObterPorId(int id)
        {
            return _context.RacaPet.Find(id);
        }
    }
}
