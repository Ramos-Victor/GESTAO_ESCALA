using Gestao_Escala.Models;
using Gestao_Escala.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Escala.Controllers
{
    [ApiController]
    [Route("api/vigencia/{vigenciaId}/motoristas")]
    public class VigenciaMotoristaController : ControllerBase
    {
        private readonly IVigenciaMotorista _service;

        public VigenciaMotoristaController(IVigenciaMotorista service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VigenciaMotorista>>> GetTodos(int vigenciaId)
        {
            var resultado = await _service.ListarPorVigenciaAsync(vigenciaId);
            return Ok(resultado);
        }

        [HttpGet("{data}")]
        public async Task<ActionResult<VigenciaMotorista>> GetPorData(int vigenciaId, DateOnly data)
        {
            var resultado = await _service.ObterPorDataAsync(vigenciaId, data);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<VigenciaMotorista>> Post(int vigenciaId, VigenciaMotorista vigenciaMotorista)
        {
            try
            {
                var criado = await _service.CriarVigenciaMotoristaAsync(vigenciaId, vigenciaMotorista);
                return CreatedAtAction(nameof(GetPorData), new { vigenciaId, data = criado.Data }, criado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int vigenciaId, int id, VigenciaMotorista vigenciaMotorista)
        {
            if (id != vigenciaMotorista.Id) return BadRequest("Id inconsistente.");

            var existente = await _service.ObterPorIdAsync(id);
            if (existente == null) return NotFound();
            if (existente.VigenciaId != vigenciaId) return BadRequest("Motorista não pertence a essa vigência.");

            await _service.AtualizarVigenciaMotoristaAsync(vigenciaMotorista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int vigenciaId, int id)
        {
            var existente = await _service.ObterPorIdAsync(id);
            if (existente == null) return NotFound();
            if (existente.VigenciaId != vigenciaId) return BadRequest("Motorista não pertence a essa vigência.");

            await _service.DeletarVigenciaMotoristaAsync(id);
            return NoContent();
        }
    }
}