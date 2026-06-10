using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class ComportamentoPetRepository : IComportamentoPetRepository
    {
        private readonly AgendaPetDbContext _context;
        public ComportamentoPetRepository(AgendaPetDbContext context) => _context = context;

        public List<ComportamentoPet> Listar()
        {
            return _context.ComportamentoPet.OrderBy(c => c.NomeComportamento).ToList();
        }

        public ComportamentoPet ObterPorId(int id)
        {
            return _context.ComportamentoPet.Find(id);
        }
    }
}
