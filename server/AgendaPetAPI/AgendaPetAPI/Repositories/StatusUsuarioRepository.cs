using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class StatusUsuarioRepository : IStatusUsuarioRepository
    {
        private readonly AgendaPetDbContext _context;
        public StatusUsuarioRepository(AgendaPetDbContext context) => _context = context;

        public List<StatusUsuario> Listar()
        {
            return _context.StatusUsuario.OrderBy(p => p.NomeStatus).ToList();
        }

        public StatusUsuario ObterPorId(Guid id)
        {
            return _context.StatusUsuario.Find(id);
        }
    }
}
