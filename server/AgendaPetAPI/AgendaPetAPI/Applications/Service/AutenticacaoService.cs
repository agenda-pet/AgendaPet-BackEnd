using AgendaPetAPI.Applications.Autentification;
using AgendaPetAPI.Applications.Conversions;
using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.AutenticacaoDTO;
using AgendaPetAPI.Exceptions;
using AgendaPetAPI.Interfaces;
using System.Runtime.CompilerServices;

namespace AgendaPetAPI.Applications.Service
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly GeradorTokenJWT _jwt;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, GeradorTokenJWT jwt)
        {
            _usuarioRepository = usuarioRepository;
            _jwt = jwt;
        }

        public static bool VerificarSenha(string senha, byte[] senhaHash)
        {
            if (senha == null) return false;

            byte[] senhaConvertida = SenhaHash.Converter(senha);
            return senhaHash.SequenceEqual(senhaConvertida);
        }

        public TokenDto Login(string email, string senha)
        {
            Usuario usuario = _usuarioRepository.ObterPorEmail(email);
            if (usuario == null)
                throw new DomainException("Email ou senha inválidos!!!");

            if (!VerificarSenha(senha, usuario.Senha))
                throw new DomainException("Email ou senha inválidos!!!");

            string token = _jwt.GerarToken(usuario);

            TokenDto tokenDto = new TokenDto
            {
                Token = token,
                NomeTipoUsuario = usuario.TipoUsuario.NomeTipo
            };

            return tokenDto;
        }
    }
}
