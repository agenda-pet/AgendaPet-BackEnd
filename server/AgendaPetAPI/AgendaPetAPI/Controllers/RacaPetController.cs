using AgendaPetAPI.Applications.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacaPetController : ControllerBase
    {
        private readonly RacaPetervice _service;
        public RacaPetController(RacaPetervice service) => _service = service;

        [HttpGet]
        public IActionResult Listar()
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

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            try
            {
                return Ok(_service.ObterPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
