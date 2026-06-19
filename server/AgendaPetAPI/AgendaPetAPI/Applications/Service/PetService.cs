using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.PetDTO;
using AgendaPetAPI.DTOs.UsuarioDTO;
using AgendaPetAPI.Exceptions;
using AgendaPetAPI.Interfaces;
using AgendaPetAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaPetAPI.Applications.Service
{
    public class PetService
    {
        private readonly IPetRepository _repository;
        private readonly ITipoAnimalRepository _tipoAnimalRepository;
        private readonly IRacaPetRepository _racaRepository;
        private readonly IComportamentoPetRepository _comportamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPortePetRepository _porteRepository;

        public PetService(IPetRepository repository, ITipoAnimalRepository tipoAnimalRepository, IRacaPetRepository racaPetRepository, IPortePetRepository porteRepository, IComportamentoPetRepository comportamentoRepository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _tipoAnimalRepository = tipoAnimalRepository;
            _racaRepository = racaPetRepository;
            _comportamentoRepository = comportamentoRepository;
            _porteRepository = porteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public List<LerPetDto> Listar()
        {
            List<Pet> Pet = _repository.Listar();
            if (Pet == null || Pet.Count == 0)
                throw new DomainException("Nenhum pet localizado!!!");

            List<LerPetDto> PetDto = Pet.Select(pet => new LerPetDto
            {
                PetID = pet.PetID,
                Nome = pet.Nome,
                PorteID = pet.PorteID,
                nomePorte = pet.Porte.NomePorte,
                RacaID = pet.RacaID,
                nomeRaca = pet.Raca.NomeRaca,
                ComportamentoID = pet.ComportamentoID,
                nomeComportamento = pet.Comportamento.NomeComportamento,
                TipoAnimalID = pet.TipoAnimalID,
                nomeTipo = pet.TipoAnimal.NomeTipo,
                UsuarioID = pet.UsuarioID,
                NomeDono = pet.Usuario.Nome,
                Agendamentos = pet.Agendamento != null
                    ? pet.Agendamento.Where(a => a.StatusAgendamento.NomeStatus != "Cancelado").Select(p => new AgendamentoPorPetDto
                    {
                        AgendamentoId = p.AgendamentoID,
                        DataAgendamento = p.DataAgendamento
                    }).ToList()
                    : new List<AgendamentoPorPetDto>()
            }).ToList();

            return PetDto;
        }

        public LerPetDto ObterPorId(Guid id)
        {
            Pet pet = _repository.ObterPorId(id);
            if (pet == null)
                throw new DomainException("Nenhum pet localizado!!!");

            LerPetDto petDto = new LerPetDto
            {
                PetID = pet.PetID,
                Nome = pet.Nome,
                PorteID = pet.PorteID,
                nomePorte = pet.Porte.NomePorte,
                RacaID = pet.RacaID,
                nomeRaca = pet.Raca.NomeRaca,
                ComportamentoID = pet.ComportamentoID,
                nomeComportamento = pet.Comportamento.NomeComportamento,
                TipoAnimalID = pet.TipoAnimalID,
                nomeTipo = pet.TipoAnimal.NomeTipo,
                UsuarioID = pet.UsuarioID,
                NomeDono = pet.Usuario.Nome,
                Agendamentos = pet.Agendamento != null
                    ? pet.Agendamento.Where(a => a.StatusAgendamento.NomeStatus != "Cancelado").Select(p => new AgendamentoPorPetDto
                    {
                        AgendamentoId = p.AgendamentoID,
                        DataAgendamento = p.DataAgendamento
                    }).ToList()
                    : new List<AgendamentoPorPetDto>()
            };

            return petDto;
        }

        public List<LerPetDto> ObterPorNome(string nome)
        {
            List<Pet> pets = _repository.ObterPorNome(nome);
            if (pets == null || pets.Count == 0)
                throw new DomainException("Nenhum pet localizado!!!");

            List<LerPetDto> petsDto = pets.Select(pet => new LerPetDto
            {
                PetID = pet.PetID,
                Nome = pet.Nome,
                nomePorte = pet.Porte.NomePorte,
                nomeRaca = pet.Raca.NomeRaca,
                nomeComportamento = pet.Comportamento.NomeComportamento,
                nomeTipo = pet.TipoAnimal.NomeTipo,
                NomeDono = pet.Usuario.Nome,
                Agendamentos = pet.Agendamento != null
                    ? pet.Agendamento.Where(a => a.StatusAgendamento.NomeStatus != "Cancelado").Select(p => new AgendamentoPorPetDto
                    {
                        AgendamentoId = p.AgendamentoID,
                        DataAgendamento = p.DataAgendamento
                    }).ToList()
                    : new List<AgendamentoPorPetDto>()
            }).ToList();
            return petsDto;
        }

        public List<LerPetDto> ObterTutorPorId(Guid tutorId)
        {
            List<Pet> pets = _repository.ObterPorTutor(tutorId);
            if (pets == null || pets.Count == 0)
                throw new DomainException("Nenhum pet localizado");

            List<LerPetDto> petDtos = pets.Select(pet => new LerPetDto
            {
                PetID = pet.PetID,
                Nome = pet.Nome,
                nomePorte = pet.Porte.NomePorte,
                nomeRaca = pet.Raca.NomeRaca,
                nomeComportamento = pet.Comportamento.NomeComportamento,
                nomeTipo = pet.TipoAnimal.NomeTipo,
                NomeDono = pet.Usuario.Nome,
            }).ToList();

            return petDtos;
        }

        public Pet Adicionar(CriarPetDto pet)
        {
            if (pet.UsuarioID == null || pet.Nome == null || pet.ComportamentoID == null || pet.PorteID == null || pet.TipoAnimalID == null || pet.RacaID == null)
                throw new DomainException("Dados inválidos!!!");

            else if (_tipoAnimalRepository.ObterPorId(pet.TipoAnimalID) == null)
                throw new DomainException("Tipo animal id inválido!!!");

            else if (_racaRepository.ObterPorId(pet.RacaID) == null)
                throw new DomainException("Raça animal id invalida!!!");

            else if (_porteRepository.ObterPorId(pet.PorteID) == null)
                throw new DomainException("Porte id invalido!!!");

            else if (_comportamentoRepository.ObterPorId(pet.ComportamentoID) == null)
                throw new DomainException("Comportamento id inválido!!!");

            else if (_usuarioRepository.ObterPorId(pet.UsuarioID) == null)
                throw new DomainException("Usuario id inválido");

            Pet petAdicionado = new Pet
            {
                Nome = pet.Nome,
                TipoAnimalID = pet.TipoAnimalID,
                RacaID = pet.RacaID,
                PorteID = pet.PorteID,
                ComportamentoID = pet.ComportamentoID,
                UsuarioID = pet.UsuarioID,
            };

            _repository.Adicionar(petAdicionado);
            return petAdicionado;
        }

        public Pet Atualizar(Guid id, AtualizarPetDto petDto)
        {
            if (petDto.UsuarioID == null || petDto.Nome == null || petDto.ComportamentoID == null)
                throw new DomainException("Dados inválidos!!!");

            else if (_comportamentoRepository.ObterPorId(petDto.ComportamentoID) == null)
                throw new DomainException("Comportamento id inválido!!!");

            else if (_usuarioRepository.ObterPorId(petDto.UsuarioID) == null)
                throw new DomainException("Usuario id inválido");

            Pet petAtualizado = _repository.ObterPorId(id);
            if (petAtualizado == null)
                throw new DomainException("Pet id inválido");

            petAtualizado.Nome = petDto.Nome;
            petAtualizado.ComportamentoID = petDto.ComportamentoID;
            petAtualizado.PorteID = petDto.PorteID;
            petAtualizado.UsuarioID = petDto.UsuarioID;

            _repository.Atualizar(id, petAtualizado);
            return petAtualizado;
        }

        public void Remover(Guid id)
        {
            Pet pet = _repository.ObterPorId(id);
            if (pet == null)
                throw new DomainException("Pet id inválido!!!");

            _repository.Remover(id);
        }

    }
}
