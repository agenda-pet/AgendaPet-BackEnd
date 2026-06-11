using AgendaPetAPI.Domains;

namespace AgendaPetAPI.Interfaces
{
    public interface IStatusAgendamentoRepository
    {
        public List<StatusAgendamento> Listar();
        public  StatusAgendamento ObterPorId(Guid id);
    }
}
