using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.TipoAnimalDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class TipoAnimalService
    {
        private readonly ITipoAnimalRepository _repository;
        public TipoAnimalService(ITipoAnimalRepository repository) => _repository = repository;

        public List<LerTipoAnimalDto> Listar()
        {
            List<TipoAnimal> tipoAnimal = _repository.Listar();
            if (tipoAnimal == null)
                throw new Exception("Nenhum tipo de animal localizado!!!");

            List<LerTipoAnimalDto> tipoAnimalDto = tipoAnimal.Select(tipo => new LerTipoAnimalDto
            {
                TipoAnimalID = tipo.TipoAnimalID,
                NomeTipo = tipo.NomeTipo,
            }).ToList();

            return tipoAnimalDto;
        }

        public LerTipoAnimalDto ObterPorId(Guid id)
        {
            TipoAnimal tipo = _repository.ObterPorId(id);
            if (tipo == null)
                throw new Exception("Nenhum tipo de animal localizado!!!");

            LerTipoAnimalDto tipoDto = new LerTipoAnimalDto
            {
                TipoAnimalID = tipo.TipoAnimalID,
                NomeTipo = tipo.NomeTipo,
            };

            return tipoDto;
        }
    }
}
