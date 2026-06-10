using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly AgendaPetDbContext _context;
        public TipoUsuarioRepository(AgendaPetDbContext context) => _context = context;

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuario.OrderBy(p => p.NomeRaca).ToList();
        }

        public TipoUsuario ObterPorId(int id)
        {
            return _context.TipoUsuario.Find(id);
        }
    }
}
