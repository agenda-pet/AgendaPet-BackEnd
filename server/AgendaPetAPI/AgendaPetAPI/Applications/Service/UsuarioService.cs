using AgendaPetAPI.Applications.Conversions;
using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.UsuarioDTO;
using AgendaPetAPI.Exceptions;
using AgendaPetAPI.Interfaces;
using System.Data;

namespace AgendaPetAPI.Applications.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        public UsuarioService(IUsuarioRepository repository, ITipoUsuarioRepository TipoUsuarioRepository)
        {
            _repository = repository;
            _tipoUsuarioRepository = TipoUsuarioRepository;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            if (usuarios == null || usuarios.Count == 0)
                throw new DomainException("Nenhum usuario localizado!!!");

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuario => new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                NumeroTelefone = usuario.NumeroTelefone,
                TipoUsuarioID = usuario.TipoUsuarioID,
                StatusUsuarioID = usuario.StatusUsuarioID,
                NomePet = usuario.Pet.Select(p => p.Nome).ToList()
            }).ToList();

            return usuariosDto;
        }

        public LerUsuarioDto ObterPorId(Guid id)
        {
            Usuario usuario = _repository.ObterPorId(id);
            if (usuario == null)
                throw new DomainException("Nenhum usuario localizado!!!");

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                NumeroTelefone = usuario.NumeroTelefone,
                TipoUsuarioID = usuario.TipoUsuarioID,
                StatusUsuarioID = usuario.StatusUsuarioID,
                NomePet = usuario.Pet.Select(p => p.Nome).ToList()
            };

            return usuarioDto;
        }

        public List<LerUsuarioDto> ObterPorNome(string nome)
        {
            List<Usuario> usuarios = _repository.ObterPorNome(nome);
            if (usuarios == null)
                throw new DomainException("Nenhum usuario localizado!!!");

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuario => new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                NumeroTelefone = usuario.NumeroTelefone,
                TipoUsuarioID = usuario.TipoUsuarioID,
                StatusUsuarioID = usuario.StatusUsuarioID,
                NomePet = usuario.Pet.Select(p => p.Nome).ToList()
            }).ToList();

            return usuariosDto;
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario usuario = _repository.ObterPorEmail(email);
            if (usuario == null)
                throw new DomainException("Nenhum usuario localizado!!!");

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                NumeroTelefone = usuario.NumeroTelefone,
                TipoUsuarioID = usuario.TipoUsuarioID,
                StatusUsuarioID = usuario.StatusUsuarioID,
                NomePet = usuario.Pet.Select(p => p.Nome).ToList()

            };

            return usuarioDto;
        }

        public LerUsuarioDto ObterPorTelefone(string telefone)
        {
            Usuario usuario = _repository.ObterPorTelefone(telefone);
            if (usuario == null)
                throw new DomainException("Nenhum usuario localizado!!!");

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                NumeroTelefone = usuario.NumeroTelefone,
                TipoUsuarioID = usuario.TipoUsuarioID,
                StatusUsuarioID = usuario.StatusUsuarioID,
                NomePet = usuario.Pet.Select(p => p.Nome).ToList()
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto usuarioDto)
        {
            if (usuarioDto == null || usuarioDto.Nome == null || usuarioDto.TipoUsuarioID == null || usuarioDto.Email == null || usuarioDto.Senha == null)
                throw new DomainException("Dados invalidos");

            if (_tipoUsuarioRepository.ObterPorId(usuarioDto.TipoUsuarioID) == null)
                throw new DomainException("Tipo usuario id invalido!!!");

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = SenhaHash.Converter(usuarioDto.Senha),
                NumeroTelefone = usuarioDto.NumeroTelefone,
                TipoUsuarioID = usuarioDto.TipoUsuarioID,
                StatusUsuarioID = usuarioDto.StatusUsuarioID,
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid id, AtualizarUsuarioDto usuarioDto)
        {
            if (usuarioDto == null || usuarioDto.Nome == null || usuarioDto.TipoUsuarioID == null || usuarioDto.Email == null)
                throw new DomainException("Dados invalidos");

            if (_tipoUsuarioRepository.ObterPorId(usuarioDto.TipoUsuarioID) == null)
                throw new DomainException("Tipo usuario id invalido!!!");

            Usuario usuario = _repository.ObterPorId(id);

            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
            usuario.NumeroTelefone = usuarioDto.NumeroTelefone;
            usuario.Senha = SenhaHash.Converter(usuarioDto.Senha) ?? usuario.Senha;
            usuario.TipoUsuarioID = usuarioDto.TipoUsuarioID;
            usuario.StatusUsuarioID = usuarioDto.StatusUsuarioID;
            
            _repository.Atualizar(id, usuario);
        }

        public void AtualizarSenha(Guid id, string senha)
        {
            if (_repository.ObterPorId(id) == null)
                throw new DomainException("Usuario id invalido!!!");

            _repository.AtualizarSenha(id, senha);
        }

        public void Remover(Guid id)
        {
            if (_repository.ObterPorId(id) == null)
                throw new DomainException("Usuario id invalido!!!");

            _repository.Remover(id);
        }
    }
}
