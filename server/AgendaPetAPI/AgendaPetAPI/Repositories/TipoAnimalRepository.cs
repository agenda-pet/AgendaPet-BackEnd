using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class TipoAnimalRepository : ITipoAnimalRepository
    {
        private readonly AgendaPetDbContext _context;
        public TipoAnimalRepository(AgendaPetDbContext context) => _context = context;

        public List<TipoAnimal> Listar()
        {
            return _context.TipoAnimal.OrderBy(p => p.NomeTipo).ToList();
        }

        public TipoAnimal ObterPorId(Guid id)
        {
            return _context.TipoAnimal.Find(id);
        }
    }
}
