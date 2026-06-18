using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Contexts;

namespace AgendaPetAPI.Repositories
{
    public class LogAgendamentoRepository : ILogAgendamentoRepository
    {
        private readonly AgendaPetDbContext _context;

        public LogAgendamentoRepository(AgendaPetDbContext context)
        {
            _context = context;
        }

        public List<LogAgendamento> Listar()
        {
            // OrderByDescending -> Ordenar por data
            List<LogAgendamento> log = _context.LogAgendamento.OrderByDescending(l => l.DataModificacao).ToList();

            return log;
        }

        public List<LogAgendamento> ListarPorID(Guid agendamentoId)
        {
            List<LogAgendamento> AlteracoesAgendamento = _context.LogAgendamento
                .Where(log => log.AgendamentoID == agendamentoId)
                .OrderByDescending(log => log.DataModificacao)
                .ToList();

            return AlteracoesAgendamento;
        }
    }
}