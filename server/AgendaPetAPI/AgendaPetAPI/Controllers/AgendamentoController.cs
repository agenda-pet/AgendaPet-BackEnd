using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.DTOs.AgendamentoDTO;
using AgendaPetAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<List<LerAgendamentoDto>> Listar()
        {
            List<LerAgendamentoDto> agendamentos = _service.Listar();
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerAgendamentoDto> ObterPorId(Guid id)
        {
            try
            {
                LerAgendamentoDto agendamento = _service.BuscarPorId(id);
                return Ok(agendamento);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Tutor/{tutorId}")]
        public ActionResult<List<LerAgendamentoDto>> ListarPorTutor(Guid tutorId)
        {
            List<LerAgendamentoDto> agendamentos = _service.ListarAgendamentoPorTutor(tutorId);
            return Ok(agendamentos);
        }

        [HttpPost]
        public ActionResult Adicionar(CriarAgendamentoDto agendamentoDto)
        {
            try
            {
                _service.Adicionar(agendamentoDto);
                return Created();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
