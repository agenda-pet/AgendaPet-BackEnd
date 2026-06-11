using AgendaPetAPI.Applications.Service;
using AgendaPetAPI.DTOs.PetDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaPetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly PetService _service;
        public PetController(PetService Petervice) => _service = Petervice;

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("TutorId/{tutorId}/PetId/{petId}")]
        public IActionResult ObterTutorPorId(Guid tutorId, Guid petId)
        {
            try
            {
                return Ok(_service.ObterTutorPorId(tutorId, petId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Adicionar(CriarPetDto petDto)
        {
            try
            {
                _service.Adicionar(petDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut ("{id}")]
        public IActionResult Atualizar(Guid id, AtualizarPetDto petDto)
        {
            try
            {
                _service.Atualizar(id, petDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
