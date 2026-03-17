using Gestao_Escala.Models;
using Microsoft.AspNetCore.Mvc;
using Gestao_Escala.Domain.Interfaces;

namespace Gestao_Escala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoristasController : ControllerBase
    {
        private readonly IMotoristaService _motoristaService;

        public MotoristasController(IMotoristaService motoristaService)
        {
            _motoristaService = motoristaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorista>>> Get([FromQuery] int page =1, [FromQuery] int pageSize = 20)
        {
            var motoristas = await _motoristaService.ListarTudoAsync(page, pageSize);
            return Ok(motoristas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetPorId(int id)
        {
            var motorista = await _motoristaService.ObterPorIdAsync(id);

            if (motorista == null)
            {
                return NotFound("Motorista não encontrado.");
            }
            
            return Ok(motorista);
        }

        [HttpPost]
        public async Task<ActionResult<Motorista>> Post(Motorista motorista)
        {
            try
            {
                var novoMotorista = await _motoristaService.CriarMotoristaAsync(motorista);
                return CreatedAtAction(nameof(GetPorId), new {id = novoMotorista.Id}, novoMotorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Motorista motorista)
        {
            if (id != motorista.Id)
            {
                return BadRequest("ID inconsistente");
            }

            try
            {
                await _motoristaService.AtualizarMotoristaAsync(motorista);
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
            var removido = await _motoristaService.DeletarMotoristaAsync(id);

            if(!removido)
                return NotFound();

            return NoContent();
        }
    }
}