using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class StatusAgendamentoRepository : IStatusAgendamentoRepository
    {
        private readonly AgendaPetDbContext _context;
        public StatusAgendamentoRepository(AgendaPetDbContext context) => _context = context;

        public List<StatusAgendamento> Listar()
        {
            return _context.StatusAgendamento.OrderBy(p => p.NomeStatus).ToList();
        }

        public  StatusAgendamento ObterPorId(Guid id)
        {
            return _context.StatusAgendamento.Find(id);
        }
    }
}
