using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaPetAPI.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AgendaPetDbContext _context;
        public PetRepository(AgendaPetDbContext context) => _context = context;

        public List<Pet> Listar() => _context.Pet.Include(p => p.Usuario)
                                                 .Include(p => p.Comportamento)
                                                 .Include(p => p.Raca).Include(p => p.TipoAnimal)
                                                 .Include(p => p.Porte)
                                                 .OrderBy(p => p.Nome).ToList();

        public Pet ObterPorId(Guid id) => _context.Pet.Find(id);
        public List<Pet> ObterPorTutor(Guid tutorId)
        {
            List<Pet> pets = _context.Pet.Include(p => p.Usuario)
                                                 .Include(p => p.Comportamento)
                                                 .Include(p => p.Raca).Include(p => p.TipoAnimal)
                                                 .Include(p => p.Porte)
                                                 .Where(t => t.UsuarioID.Equals(tutorId)).ToList();
            return pets;
        }
        public List<Pet> ObterPorNome(string nome)
        {
            List<Pet> pets = _context.Pet.Include(p => p.Usuario)
                                                 .Include(p => p.Comportamento)
                                                 .Include(p => p.Raca).Include(p => p.TipoAnimal)
                                                 .Include(p => p.Porte)
                                                 .Where(t => t.Nome.Contains(nome)).ToList();
            return pets;
        }

        public void Adicionar(Pet pet)
        {
            if (pet == null)
                return;

            _context.Pet.Add(pet);
            _context.SaveChanges();
        }

        public void Atualizar(Guid id, Pet pet)
        {
            Pet petBanco = _context.Pet.Find(id);
            if (petBanco == null)
                return;

            petBanco.PetID = pet.PetID;
            petBanco.Nome = pet.Nome;
            petBanco.TipoAnimalID = pet.TipoAnimalID;
            petBanco.RacaID = pet.RacaID;
            petBanco.PorteID = pet.PorteID;
            petBanco.ComportamentoID = pet.ComportamentoID;
            petBanco.UsuarioID = pet.UsuarioID;

            _context.Update(petBanco);
            _context.SaveChanges();
        }

        public void Remover(Guid id)
        {
            Pet pet = _context.Pet.Find(id);
            if (pet == null) return;

            _context.Pet.Remove(pet);
            _context.SaveChanges();
        }
    }
}
