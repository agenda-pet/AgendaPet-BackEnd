using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.AgendamentoDTO;
using AgendaPetAPI.Exceptions;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IPetRepository _petRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IStatusAgendamentoRepository _statusRepository;
        public AgendamentoService(IAgendamentoRepository repository, IServicoRepository servicoRepository, IPetRepository petRepository, IUsuarioRepository usuarioRepository, IStatusAgendamentoRepository statusRepository)
        {
            _repository = repository;
            _servicoRepository = servicoRepository;
            _petRepository = petRepository;
            _usuarioRepository = usuarioRepository;
            _statusRepository = statusRepository;
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

        public void Adicionar(CriarAgendamentoDto agendamentoDto, Guid funcionarioLogadoID)
        {
            if(agendamentoDto.ServicosIds == null || !agendamentoDto.ServicosIds.Any())
            {
                throw new DomainException("É necessário colocar pelo menos um serviço para o agendamento.");
            }

            if(_petRepository.ObterPorId(agendamentoDto.PetID) == null)
            {
                throw new DomainException("O Pet informado não existe no sistema");
            }

            DateTime dataHoraAgendamento = agendamentoDto.DataAgendamento.ToDateTime(agendamentoDto.HoraAgendamento);
            if(dataHoraAgendamento < DateTime.Now)
            {
                throw new DomainException("Não é possível adicionar um agendamento em uma data ou hora passado.");
            }

            bool funcionarioOcupado = _repository.Listar()
                .Any(a => a.FuncionarioID == funcionarioLogadoID &&
                          a.DataAgendamento == agendamentoDto.DataAgendamento &&
                          a.HoraAgendamento == agendamentoDto.HoraAgendamento &&
                          a.StatusAgendamento.NomeStatus != "Cancelado");

            if(funcionarioOcupado)
            {
                throw new DomainException("Este funcionário já possui um agendamento neste mesmo dia e horário.");
            }

            List<Servico> listaServicosSelecionados = new List<Servico>();
            decimal valorTotalCalculado = 0;
            int tempoTotalCalculado = 0;

            foreach (var servicoID in agendamentoDto.ServicosIds)
            {
                Servico servicoBanco = _servicoRepository.ObterPorId(servicoID);
            
                if(servicoBanco == null)
                {
                    throw new DomainException("Serviço não encontrado.");
                }

                listaServicosSelecionados.Add(servicoBanco);
                valorTotalCalculado += servicoBanco.Preco;
                tempoTotalCalculado += servicoBanco.TempoServico;
            }

            Agendamento agendamento = new Agendamento
            {
                AgendamentoID = Guid.NewGuid(),
                DataAgendamento = agendamentoDto.DataAgendamento,
                HoraAgendamento = agendamentoDto.HoraAgendamento,
                StatusAgendamentoID = agendamentoDto.StatusAgendamentoID,
                FuncionarioID = funcionarioLogadoID,
                PetID = agendamentoDto.PetID,
                ValorTotal = valorTotalCalculado,
                TempoTotal = tempoTotalCalculado,
                Servico = listaServicosSelecionados
            };

            _repository.Adicionar(agendamento);

            LogAgendamento logInicial = new LogAgendamento
            {
                LogAgendamentoID = Guid.NewGuid(),
                AgendamentoID = agendamento.AgendamentoID,
                DataModificacao = DateTime.Now,
                DataAnteriorAgendameto = agendamento.DataAgendamento.ToDateTime(agendamento.HoraAgendamento),

                StatusAgendamentoAnterior = "Pendente",
                ServicosPorAgendamento = string.Join(", ", listaServicosSelecionados.Select(s => s.NomeServico))
            };

            _repository.AdicionarLog(logInicial);
        }

        public void Atualizar(Guid agendamentoID, AtualizarAgendamentoDto agendamentoDto, Guid funcionarioLogadoID)
        {
            Agendamento agendamentoBanco = _repository.BuscarPorId(agendamentoID);
            if (agendamentoBanco == null)
            {
                throw new DomainException("O Agendamento informado não foi encontrado.");
            }

            if (agendamentoDto.ServicosIds == null || !agendamentoDto.ServicosIds.Any())
            {
                throw new DomainException("É necessário colocar pelo menos um serviço para o agendamento.");
            }

            if (_petRepository.ObterPorId(agendamentoDto.PetID) == null)
            {
                throw new DomainException("O Pet informado não existe no sistema");
            }

            DateTime novaDataHora = agendamentoDto.DataAgendamento.ToDateTime(agendamentoDto.HoraAgendamento);
            if (novaDataHora < DateTime.Now)
            {
                throw new DomainException("Não é possível alterar um agendamento para uma data ou horário passado.");
            }

            bool horarioOcupado = _repository.Listar()
                .Any(a => a.FuncionarioID == funcionarioLogadoID &&
                          a.DataAgendamento == agendamentoDto.DataAgendamento &&
                          a.HoraAgendamento == agendamentoDto.HoraAgendamento &&
                          a.AgendamentoID != agendamentoID &&
                          a.StatusAgendamento.NomeStatus != "Cancelado");

            if (horarioOcupado)
            {
                throw new DomainException("Você já possui outro agendamento marcado para este mesmo dia e horário.");
            }

            List<Servico> novosServicos = new List<Servico>();
            decimal novoValorTotal = 0;
            int novoTempoTotal = 0;

            foreach (var servicoId in agendamentoDto.ServicosIds)
            {
                var servicoBanco = _servicoRepository.ObterPorId(servicoId);
                if (servicoBanco == null)
                {
                    throw new DomainException($"Serviço não encontrado.");
                }

                novosServicos.Add(servicoBanco);
                novoValorTotal += servicoBanco.Preco;
                novoTempoTotal += servicoBanco.TempoServico;
            }

            Agendamento agendamentoAtualizado = new Agendamento
            {
                DataAgendamento = agendamentoDto.DataAgendamento,
                HoraAgendamento = agendamentoDto.HoraAgendamento,
                StatusAgendamentoID = agendamentoDto.StatusAgendamentoID,
                FuncionarioID = funcionarioLogadoID,
                PetID = agendamentoDto.PetID,
                ValorTotal = novoValorTotal,
                TempoTotal = novoTempoTotal,
                Servico = novosServicos
            };

            _repository.Atualizar(agendamentoAtualizado);
        }

        public void Cancelar(Guid agendamentoId)
        {
            Agendamento agendamentoBanco = _repository.BuscarPorId(agendamentoId);
            if(agendamentoBanco == null)
            {
                throw new DomainException("Agendamento não encontrado.");
            }

            if(agendamentoBanco.StatusAgendamento.NomeStatus == "Concluído")
            {
                throw new DomainException("Não é possível cancelar um agendamento que já foi concluído.");
            }

            if (agendamentoBanco.StatusAgendamento.NomeStatus == "Cancelado")
            {
                throw new DomainException("Este agendamento já está cancelado.");
            }

            StatusAgendamento? statusCancelado = _statusRepository.Listar().FirstOrDefault(s => s.NomeStatus == "Cancelado");
        
            if(statusCancelado == null)
            {
                throw new DomainException("O status 'Cancelado' não foi encontrado no sistema.");
            }

            _repository.Cancelar(agendamentoId, statusCancelado.StatusAgendamentoID);
        }
    }
}
