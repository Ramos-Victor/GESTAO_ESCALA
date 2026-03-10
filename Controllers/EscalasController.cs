using Gestao_Escala.Models;
using Gestao_Escala.Services; 
using Microsoft.AspNetCore.Mvc;
using Gestao_Escala.Domain.Interfaces;
namespace Gestao_Escala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscalasController : ControllerBase
    {
        private readonly IEscalaService _escalaService;

        public EscalasController(IEscalaService escalaService)
        {
            _escalaService = escalaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Escala>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var escalas = await _escalaService.ListarTudoAsync(page, pageSize);
            return Ok(escalas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Escala>> GetPorId(int id)
        {
            var escala = await _escalaService.ObterPorIdAsync(id);

            if (escala == null)
            {
                return NotFound("Escala não encontrada.");
            }

            return Ok(escala);
        }

        [HttpPost]
        public async Task<ActionResult<Escala>> Post(Escala escala)
        {
            try
            {
                var novaEscala = await _escalaService.CriarEscalaAsync(escala);
                return CreatedAtAction(nameof(GetPorId), new { id = novaEscala.Id }, novaEscala);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Escala escala)
        {
            if (id != escala.Id) return BadRequest("ID inconsistente.");

            try
            {
                await _escalaService.AtualizarEscalaAsync(escala);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removido = await _escalaService.DeletarEscalaAsync(id);
            
            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}