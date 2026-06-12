using AgendaPetAPI.Controllers;
using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.LerLogAgendamentoDto;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class LogAgendamentoService
    {
        private readonly ILogAgendamentoRepository _repository;

        public LogAgendamentoService(ILogAgendamentoRepository repository)
        {

            _repository = repository;
        }

        public List<LerAgendamentoDto> Listar()
        {
            List<LogAgendamento> logs = _repository.Listar();

            List<LerAgendamentoDto> listaLogAgendamento = logs.Select(log => new LerAgendamentoDto
            {
                LogAgendamentoID = log.LogAgendamentoID,
                DataModificacao = log.DataModificacao,
                DataAnteriorAgendamento = log.DataAnteriorAgendamento,
                StatusAgendamentoAnterior = log.StatusAgendamentoAnterior,
                ServicosPorAgendamento = log.ServicosPorAgendamento,
                AgendamentoID = log.AgendamentoID,
                Agendamento = log.Agendamento
            }).ToList();

            return listaLogAgendamento;
        }

        public List<LerAgendamentoDto> ListarPorID(Guid agendamentoId)
        {
            List<LogAgendamento> logs = _repository.ListarPorID(agendamentoId);

            List<LerAgendamentoDto> listaLogAgendamento = logs.Select(log => new LerAgendamentoDto
            {
                LogAgendamentoID = log.LogAgendamentoID,
                DataModificacao = log.DataModificacao,
                DataAnteriorAgendamento = log.DataAnteriorAgendamento,
                StatusAgendamentoAnterior = log.StatusAgendamentoAnterior,
                ServicosPorAgendamento = log.ServicosPorAgendamento,
                AgendamentoID = log.AgendamentoID,
                Agendamento = log.Agendamento
            }).ToList();

            return listaLogAgendamento;
        }

    }
}
