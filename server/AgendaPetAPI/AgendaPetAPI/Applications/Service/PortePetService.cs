using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.PortePetDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class PortePetService
    {
        private readonly IPortePetRepository _repository;
        public PortePetService(IPortePetRepository repository) => _repository = repository;

        public List<LerPortePetDto> Listar()
        {
            List<PortePet> portesPet = _repository.Listar();
            if (portesPet != null)
                throw new Exception("Nenhum porte de pet localizado!!!");

            List<LerPortePetDto> portesPetDto = portesPet.Select(porte => new LerPortePetDto
            {
                PorteID = porte.PorteID,
                NomePorte = porte.NomePorte,
            }).ToList();

            return portesPetDto;
        }

        public LerPortePetDto ObterPorId(Guid id)
        {
            PortePet porte = _repository.ObterPorId(id);
            if (porte != null)
                throw new Exception("Nenhum porte localizado!!!");

            LerPortePetDto porteDto = new LerPortePetDto
            {
                PorteID = porte.PorteID,
                NomePorte = porte.NomePorte,
            };

            return porteDto;
        }
    }
}
