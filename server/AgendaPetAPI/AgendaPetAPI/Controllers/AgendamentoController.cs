using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.DTOs.AgendamentoDTO;
using AgendaPetAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendamentoService _service;

        public AgendamentoController(AgendamentoService service)
        {
            _service = service;
        }

        private Guid ObterUsuarioLogado()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("Usuário não autenticado");
            }

            return Guid.Parse(idTexto);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<LerAgendamentoDto>> Listar()
        {
            List<LerAgendamentoDto> agendamentos = _service.Listar();
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<LerAgendamentoDto> ObterPorId(Guid id)
        {
            try
            {
                LerAgendamentoDto agendamento = _service.BuscarPorId(id);
                return Ok(agendamento);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Tutor/{tutorId}")]
        [Authorize]
        public ActionResult<List<LerAgendamentoDto>> ListarPorTutor(Guid tutorId)
        {
            List<LerAgendamentoDto> agendamentos = _service.ListarAgendamentoPorTutor(tutorId);
            return Ok(agendamentos);
        }

        [HttpPost]
        [Authorize(Roles = "Funcionário")]
        public ActionResult Adicionar(CriarAgendamentoDto agendamentoDto)
        {
            try
            {
                Guid funcionarioUserID = ObterUsuarioLogado();

                _service.Adicionar(agendamentoDto, funcionarioUserID);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Funcionário")]
        public ActionResult Atualizar(Guid id, AtualizarAgendamentoDto agendamentoDto)
        {
            try
            {
                Guid funcionarioUserID = ObterUsuarioLogado();

                _service.Atualizar(id, agendamentoDto, funcionarioUserID);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("AtualizarStatusAgendamento/{id}")]
        public IActionResult AtualizarStatusAgendamento(Guid id, AtualizarStatusAgendamentoDto atualizarStatusDto)
        {
            try
            {
                _service.AtualizarStatusAgendamento(id, atualizarStatusDto.nomeStatusAgendamento);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Funcionário")]
        public ActionResult Cancelar(Guid id)
        {
            try
            {
                _service.Cancelar(id);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
