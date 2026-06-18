using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.DTOs.AutenticacaoDTO;
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
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                return Ok(_service.Login(loginDto.email, loginDto.senha));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}