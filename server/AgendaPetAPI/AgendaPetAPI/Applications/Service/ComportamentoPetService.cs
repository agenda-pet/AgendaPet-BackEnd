using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.ComportamentoPetDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class ComportamentoPetervice
    {
        private readonly IComportamentoPetRepository _repository;
        public ComportamentoPetervice(IComportamentoPetRepository repository) => _repository = repository;

        public List<LerComportamentoPetDto> Listar()
        {
            List<ComportamentoPet> comportamentos = _repository.Listar();
            if (comportamentos == null)
                throw new Exception("Nenhum comportamento localizado!!!");

            List<LerComportamentoPetDto> comportamentosDto = comportamentos.Select(comportamento => new LerComportamentoPetDto
            {
                ComportamentoID = comportamento.ComportamentoID,
                NomeComportamento = comportamento.NomeComportamento,
            }).ToList();

            return comportamentosDto;
        }

        public LerComportamentoPetDto ObterPorId(Guid id)
        {
            ComportamentoPet comportamento = _repository.ObterPorId(id);
            if (comportamento == null)
                throw new Exception("Nenhum comportamento localizado!!!");

            LerComportamentoPetDto comportamentoDto = new LerComportamentoPetDto
            {
                 ComportamentoID = comportamento.ComportamentoID,
                 NomeComportamento = comportamento.NomeComportamento,
            };

            return comportamentoDto;
        }
    }
}
 