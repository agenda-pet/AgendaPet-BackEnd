using AgendaPetAPI.Applications.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAgendamentoController : ControllerBase
    {
        private readonly LogAgendamentoService _service;

        public LogAgendamentoController(LogAgendamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Funcionário")]
        public ActionResult Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("produto/{id}")]
        [Authorize(Roles = "Funcionário")]
        public ActionResult ListarID(Guid id)
        {
            try
            {
                return Ok(_service.ListarPorID(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}