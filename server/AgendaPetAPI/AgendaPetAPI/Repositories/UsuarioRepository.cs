using AgendaPetAPI.Applications.Conversions;
using AgendaPetAPI.Contexts;
using AgendaPetAPI.Domains;
using AgendaPetAPI.Interfaces;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AgendaPetAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AgendaPetDbContext _context;
        public UsuarioRepository(AgendaPetDbContext context) => _context = context;

        public List<Usuario> Listar() => _context.Usuario.Include(u => u.TipoUsuario)
                                                         .Include(u => u.Agendamento)
                                                         .Include(u => u.Pet)
                                                         .OrderBy(u => u.Nome).ToList();

        public Usuario ObterPorId(Guid id) => _context.Usuario.Find(id);

        public List<Usuario> ObterPorNome(string nome) => _context.Usuario.Include(u => u.TipoUsuario)
                                                         .Include(u => u.Agendamento)
                                                         .Include(u => u.Pet).OrderBy(u => u.Nome).Where(u => u.Nome.Contains(nome)).ToList();

        public Usuario ObterPorEmail(string email) => _context.Usuario.Include(u => u.TipoUsuario)
                                                         .Include(u => u.Agendamento)
                                                         .Include(u => u.Pet).FirstOrDefault(u => u.Email == email);

        public Usuario ObterPorTelefone(string telefone) => _context.Usuario.Include(u => u.TipoUsuario)
                                                         .Include(u => u.Agendamento)
                                                         .Include(u => u.Pet).FirstOrDefault(u => u.NumeroTelefone == telefone);

        public void Adicionar(Usuario usuario)
        {
            if (usuario == null)
                return;

            _context.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Guid id, Usuario usuario)
        {
            Usuario usuarioBanco = _context.Usuario.Find(id);
            if (usuarioBanco == null)
                return;

            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.NumeroTelefone = usuario.NumeroTelefone;

            _context.Update(usuarioBanco);
            _context.SaveChanges();
        }

        public void AtualizarSenha(string email, string senha)
        {
            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
                return;

            usuario.Senha = SenhaHash.Converter(senha);
            _context.Update(usuario);
            _context.SaveChanges();
        }

        public void Remover(Guid id)
        {
            Usuario usuario = _context.Usuario.Find(id);
            if (usuario == null)
                return;

            _context.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
