using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IAgendamentoRepository
    {
        public List<Agendamento> Listar();
        public List<Agendamento> ListarAgendamentoPorTutor(Guid tutorId);
        public Agendamento BuscarPorId(Guid agendamentoId);
        void Adicionar(Agendamento agendamento);
        void Atualizar(Agendamento agendamento);
        void AtualizarStatusAgendamento(Guid idAgendamento, Guid statusAgendamentoId);
        void AdicionarLog(LogAgendamento logAgendamento);
        void Cancelar(Guid agendamentoId, Guid statusAgendamentoId);
    }
}
