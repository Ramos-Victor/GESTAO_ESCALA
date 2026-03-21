using Microsoft.AspNetCore.Mvc;
using Gestao_Escala.interfaces;
using Gestao_Escala.Models;

namespace Gestao_Escala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VigenciaController : ControllerBase
    {
        private readonly IVigencia _vigenciaService;

        public VigenciaController(IVigencia Vigencia)
        {
            _vigenciaService = Vigencia;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vigencia>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var Vigencia = await _vigenciaService.ListarTudoAsync(page, pageSize);
            return Ok(Vigencia);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vigencia>> GetPorId(int id)
        {
            var Vigencia = await _vigenciaService.ObterPorIdAsync(id);

            if (Vigencia == null)
            {
                return NotFound("Vigência não encontrada.");
            }

            return Ok(Vigencia);
        }

        [HttpPost]
        public async Task<ActionResult<Vigencia>> Post(Vigencia vigencia)
        {
            try
            {
                var novaVigencia = await _vigenciaService.CriarVigenciaAsync(vigencia);
                return CreatedAtAction(nameof(GetPorId), new {id = novaVigencia.Id}, novaVigencia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, Vigencia vigencia)
        {
            if (id != vigencia.Id) return BadRequest("ID inconsistente.");

            try
            {
                await _vigenciaService.AtualizarVigenciaAsync(vigencia);
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
            var removido = await _vigenciaService.DeletarVigenciaAsync(id);

            if(!removido)
                return NotFound();

            return NoContent();    
        }
    }
}