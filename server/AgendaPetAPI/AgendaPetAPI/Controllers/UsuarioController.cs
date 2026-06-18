using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.DTOs.UsuarioDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService suarioService) => _service = suarioService;

        [HttpGet]
        [Authorize]
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

        [HttpGet("UsuarioId/{id}")]
        [Authorize]
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

        [HttpGet("UsuarioNome/{nome}")]
        [Authorize]
        public IActionResult ObterPorNome(string nome)
        {
            try
            {
                return Ok(_service.ObterPorNome(nome));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UsuarioEmail/{email}")]
        [Authorize]
        public IActionResult ObterPorEmail(string email)
        {
            try
            {
                return Ok(_service.ObterPorEmail(email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UsuarioTelefone/{telefone}")]
        [Authorize]
        public IActionResult ObterPorTelefone(string telefone)
        {
            try
            {
                return Ok(_service.ObterPorTelefone(telefone));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Funcionário")]
        public IActionResult Adicionar(CriarUsuarioDto criarUsuarioDto)
        {
            try
            {
                _service.Adicionar(criarUsuarioDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Funcionário")]
        public IActionResult Atualizar(Guid id, AtualizarUsuarioDto usuarioDto)
        {
            try
            {
                _service.Atualizar(id, usuarioDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Funcionário")]
        public IActionResult AtualizarSenha(Guid id, string senha)
        {
            try
            {
                _service.AtualizarSenha(id, senha);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Funcionário")]
        public IActionResult Remover(Guid id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
