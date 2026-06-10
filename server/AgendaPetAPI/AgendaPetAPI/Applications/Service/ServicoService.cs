using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.ServicoDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Aplications.Service
{
    public class ServicoService
    {
        private readonly IServicoRepository _repository;
        public ServicoService(IServicoRepository repository) => _repository = repository;

        public List<LerServicoDto> Listar()
        {
            List<Servico> servicosPet = _repository.Listar();
            if (servicosPet != null)
                throw new Exception("Nenhum servico localizado!!!");

            List<LerServicoDto> servicosPetDto = servicosPet.Select(servico => new LerServicoDto
            {
                ServicoID = servico.ServicoID,
                NomeServico = servico.NomeServico,
            }).ToList();

            return servicosPetDto;
        }

        public LerServicoDto ObterPorId(int id)
        {
            Servico servico = _repository.ObterPorId(id);
            if (servico != null)
                throw new Exception("Nenhum servico localizado!!!");

            LerServicoDto servicoDto = new LerServicoDto
            {
                ServicoID = servico.ServicoID,
                NomeServico = servico.NomeServico,
            };

            return servicoDto;
        }
    }
}
