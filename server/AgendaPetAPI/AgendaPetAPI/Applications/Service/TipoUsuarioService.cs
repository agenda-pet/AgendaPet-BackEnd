using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.TipoUsuarioDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Applications.Service
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;
        public TipoUsuarioService(ITipoUsuarioRepository repository) => _repository = repository;

        public List<LerTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipoUsuario = _repository.Listar();
            if (tipoUsuario == null)
                throw new Exception("Nenhum tipo de usuario localizado!!!");

            List<LerTipoUsuarioDto> tipoUsuarioDto = tipoUsuario.Select(tipo => new LerTipoUsuarioDto
            {
                TipoUsuarioID = tipo.TipoUsuarioID,
                NomeTipo = tipo.NomeTipo,
            }).ToList();

            return tipoUsuarioDto;
        }

        public LerTipoUsuarioDto ObterPorId(Guid id)
        {
            TipoUsuario tipo = _repository.ObterPorId(id);
            if (tipo == null)
                throw new Exception("Nenhum tipo de usuario localizado!!!");

            LerTipoUsuarioDto tipoDto = new LerTipoUsuarioDto
            {
                TipoUsuarioID = tipo.TipoUsuarioID,
                NomeTipo = tipo.NomeTipo,
            };

            return tipoDto;
        }
    }
}
