using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface ILogAgendamentoRepository
    {
        List<LogAgendamento> Listar();
        List<LogAgendamento> ListarPorID(Guid agendamentoId);
    }
}