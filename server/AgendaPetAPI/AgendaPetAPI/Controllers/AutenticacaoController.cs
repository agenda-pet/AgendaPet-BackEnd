using AgendaPetAPI.Applications.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;
        public AutenticacaoController(AutenticacaoService service) => _service = service;

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            try
            {
                return Ok(_service.Login(email, senha));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}