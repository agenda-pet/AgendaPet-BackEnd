using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.RacaPetDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class RacaPetervice
    {
        private readonly IRacaPetRepository _repository;
        public RacaPetervice(IRacaPetRepository repository) => _repository = repository;

        public List<LerRacaPetDto> Listar()
        {
            List<RacaPet> racasPet = _repository.Listar();
            if (racasPet == null)
                throw new Exception("Nenhuma raça localizada!!!");

            List<LerRacaPetDto> racasPetDto = racasPet.Select(raca => new LerRacaPetDto
            {
                RacaID = raca.RacaID,
                NomeRaca = raca.NomeRaca,
            }).ToList();

            return racasPetDto;
        }

        public LerRacaPetDto ObterPorId(Guid id)
        {
            RacaPet raca = _repository.ObterPorId(id);
            if (raca== null)
                throw new Exception("Nenhuma raca localizada!!!");

            LerRacaPetDto racaDto = new LerRacaPetDto
            {
                RacaID = raca.RacaID,
                NomeRaca = raca.NomeRaca,
            };

            return racaDto;
        }
    }
}
