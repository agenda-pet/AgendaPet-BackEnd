using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.StatusAgendamentoDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class StatusAgendamentoService
    {
        private readonly IStatusAgendamentoRepository _repository;
        public StatusAgendamentoService(IStatusAgendamentoRepository repository) => _repository = repository;

        public List<LerStatusAgendamentoDto> Listar()
        {
            List<StatusAgendamento> statusAgendamentos = _repository.Listar();
            if (statusAgendamentos == null)
                throw new Exception("Nenhum status de agendamento localizado!!!");

            List<LerStatusAgendamentoDto> statusAgedamentoDto = statusAgendamentos.Select(statusAgendamento => new LerStatusAgendamentoDto
            {
                StatusAgendamentoID = statusAgendamento.StatusAgendamentoID,
                NomeStatus = statusAgendamento.NomeStatus,
            }).ToList();

            return statusAgedamentoDto;
        }

        public LerStatusAgendamentoDto ObterPorId(Guid id)
        {
            StatusAgendamento statusAgendamento = _repository.ObterPorId(id);
            if (statusAgendamento == null)
                throw new Exception("Nenhum status de agendamento localizado!!!");

            LerStatusAgendamentoDto statusAgendamentoDto = new LerStatusAgendamentoDto
            {
                StatusAgendamentoID = statusAgendamento.StatusAgendamentoID,
                NomeStatus = statusAgendamento.NomeStatus,
            };

            return statusAgendamentoDto;
        }

        public LerStatusAgendamentoDto ObterPorNome(string nomeStatus)
        {
            StatusAgendamento statusAgendamento = _repository.ObterPorNome(nomeStatus);
            if (statusAgendamento == null)
                throw new Exception("Nenhum status de agendamento localizado!!!");

            LerStatusAgendamentoDto statusAgendamentoDto = new LerStatusAgendamentoDto
            {
                StatusAgendamentoID = statusAgendamento.StatusAgendamentoID,
                NomeStatus = statusAgendamento.NomeStatus,
            };

            return statusAgendamentoDto;

        }
    }
}
