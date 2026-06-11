using AgendaPetAPI.Domains;
using AgendaPetAPI.DTOs.StatusUsuarioDTO;
using AgendaPetAPI.Interfaces;

namespace AgendaPetAPI.Aplications.Service
{
    public class StatusUsuarioService
    {
        private readonly IStatusUsuarioRepository _repository;
        public StatusUsuarioService(IStatusUsuarioRepository repository) => _repository = repository;

        public List<LerStatusUsuarioDto> Listar()
        {
            List<StatusUsuario> statusUsuarios = _repository.Listar();
            if (statusUsuarios != null)
                throw new Exception("Nenhum status de usuario localizado!!!");

            List<LerStatusUsuarioDto> statusUsuarioDto = statusUsuarios.Select(status => new LerStatusUsuarioDto
            {
                StatusUsuarioID = status.StatusUsuarioID,
                NomeStatus = status.NomeStatus,
            }).ToList();

            return statusUsuarioDto;
        }

        public LerStatusUsuarioDto ObterPorId(Guid id)
        {
            StatusUsuario status = _repository.ObterPorId(id);
            if (status != null)
                throw new Exception("Nenhum status de usuario localizado!!!");

            LerStatusUsuarioDto statusDto = new LerStatusUsuarioDto
            {
                StatusUsuarioID = status.StatusUsuarioID,
                NomeStatus = status.NomeStatus,
            };

            return statusDto;
        }
    }
}
