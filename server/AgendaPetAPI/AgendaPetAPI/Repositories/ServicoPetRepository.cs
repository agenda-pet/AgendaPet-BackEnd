using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class ServicoPetRepository : IServicoRepository
    {
        private readonly AgendaPetDbContext _context;
        public ServicoPetRepository(AgendaPetDbContext context) => _context = context;

        public List<Servico> Listar()
        {
            return _context.Servico.OrderBy(p => p.NomeServico).ToList();
        }

        public Servico ObterPorId(Guid id)
        {
            return _context.Servico.Find(id);
        }
    }
}
