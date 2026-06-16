using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.AgendamentoDTO;
using AgendaPetAPI.Exceptions;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public List<LerAgendamentoDto> Listar()
        {
            List<Agendamento> agendamentos = _repository.Listar();
            return agendamentos.Select(a => TransformarEmDto(a)).ToList();
        }

        public List<LerAgendamentoDto> ListarAgendamentoPorTutor(Guid tutorId)
        {
            List<Agendamento> agendamentos = _repository.ListarAgendamentoPorTutor(tutorId);
            return agendamentos.Select(a => TransformarEmDto(a)).ToList();
        }

        public LerAgendamentoDto BuscarPorId(Guid agendamentoId)
        {
            Agendamento agendamento = _repository.BuscarPorId(agendamentoId);

            if(agendamento == null)
            {
                throw new DomainException("Agendamento não encontrado");
            }

            return TransformarEmDto(agendamento);
        }

        private LerAgendamentoDto TransformarEmDto(Agendamento a)
        {
            return new LerAgendamentoDto
            {
                AgendamentoID = a.AgendamentoID,
                DataAgendamento = a.DataAgendamento,
                HoraAgendamento = a.HoraAgendamento,
                ValorTotal = a.ValorTotal,
                TempoTotal = a.TempoTotal,

                StatusAgendamentoID = a.StatusAgendamentoID,
                NomeStatus = a.StatusAgendamento?.NomeStatus ?? "Não Informado",

                FuncionarioID = a.FuncionarioID,
                NomeFuncionario = a.Funcionario?.Nome ?? "Não Informado",

                PetID = a.PetID,
                NomePet = a.Pet?.Nome ?? "Não Informado",
                NomeRaca = a.Pet?.Raca?.NomeRaca ?? "Não Informado",
                NomePorte = a.Pet?.Porte?.NomePorte ?? "Não Informado",

                TutorID = a.Pet != null ? a.Pet.UsuarioID : Guid.Empty,
                NomeTutor = a.Pet?.Usuario?.Nome ?? "Não Informado",
                TelefoneTutor = a.Pet?.Usuario?.NumeroTelefone ?? "Não Informado",

                ServicosPrestados = a.Servico != null && a.Servico.Any()
                    ? string.Join(", ", a.Servico.Select(s => s.NomeServico))
                    : "Nenhum serviço associado"
            };
        }
    }
}
