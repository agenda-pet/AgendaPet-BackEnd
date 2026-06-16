using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaPetAPI.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AgendaPetDbContext _context;

        public AgendamentoRepository(AgendaPetDbContext context)
        {
            _context = context;
        }

        public List<Agendamento> Listar()
        {
           return _context.Agendamento.Include(a => a.Pet)
                                    .ThenInclude(p => p.Raca)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Porte)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Usuario)
                                .Include(a => a.Funcionario)
                                .Include(a => a.StatusAgendamento)
                                .Include(a => a.Servico)
                                .ToList();
        
        }
        
        public List<Agendamento> ListarAgendamentoPorTutor(Guid tutorId)
        {
            return _context.Agendamento.Include(a => a.Pet)
                                    .ThenInclude(p => p.Raca)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Porte)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Usuario)
                                .Include(a => a.Funcionario)
                                .Include(a => a.StatusAgendamento)
                                .Include(a => a.Servico)
                                .Where(a => a.Pet.UsuarioID == tutorId)
                                .ToList();
        }

        public Agendamento BuscarPorId(Guid agendamentoId)
        {
            return _context.Agendamento.Include(a => a.Pet)
                                    .ThenInclude(p => p.Raca)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Porte)
                                .Include(a => a.Pet)
                                    .ThenInclude(p => p.Usuario)
                                .Include(a => a.Funcionario)
                                .Include(a => a.StatusAgendamento)
                                .Include(a => a.Servico)
                                .FirstOrDefault(a => a.AgendamentoID == agendamentoId);
        }

        public void Adicionar(Agendamento agendamento)
        {
            _context.Agendamento.Add(agendamento);
            _context.SaveChanges();
        }

        public void Atualizar(Agendamento agendamento)
        {
            Agendamento? agendamentoBanco = _context.Agendamento
                                                   .Include(a => a.StatusAgendamento)
                                                   .Include(a => a.Servico)
                                                   .FirstOrDefault(a => a.AgendamentoID == agendamento.AgendamentoID);

            if(agendamentoBanco == null)
            {
                return;
            }

            LogAgendamento log = new LogAgendamento
            {
                LogAgendamentoID = Guid.NewGuid(),
                AgendamentoID = agendamentoBanco.AgendamentoID,
                DataModificacao = DateTime.Now,
                DataAnteriorAgendameto = agendamentoBanco.DataAgendamento.ToDateTime(agendamentoBanco.HoraAgendamento),
                StatusAgendamentoAnterior = agendamentoBanco.StatusAgendamento.NomeStatus,
                ServicosPorAgendamento = string.Join(", ", agendamentoBanco.Servico.Select(s => s.NomeServico))
            };

            agendamentoBanco.DataAgendamento = agendamento.DataAgendamento;
            agendamentoBanco.HoraAgendamento = agendamento.HoraAgendamento;
            agendamentoBanco.ValorTotal = agendamento.ValorTotal;
            agendamentoBanco.StatusAgendamentoID = agendamento.StatusAgendamentoID;
            agendamentoBanco.FuncionarioID = agendamento.FuncionarioID;
            agendamentoBanco.PetID = agendamento.PetID;
            agendamentoBanco.TempoTotal = agendamento.TempoTotal;

            if(agendamento.Servico != null)
            {
                agendamentoBanco.Servico = agendamento.Servico;
            }

            AdicionarLog(log);
            _context.SaveChanges();
        }

        public void AdicionarLog(LogAgendamento logAgendamento)
        {
            _context.LogAgendamento.Add(logAgendamento);
            _context.SaveChanges();
        }
    }
}
