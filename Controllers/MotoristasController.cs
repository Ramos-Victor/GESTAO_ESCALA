using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Escala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoristasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoristasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Motorista>>> Get()
        {
            return await _context.Motorista.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetPorId(int id)
        {
            var motorista = await _context.Motorista.FindAsync(id);

            if (motorista == null)
            {
                return NotFound();
            }
            
            return motorista;
        }

        [HttpPost]
        public async Task<ActionResult<Motorista>> Post(Motorista motorista)
        {
            _context.Motorista.Add(motorista);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPorId), new { id = motorista.Id}, motorista);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Motorista motorista)
        {
            if (id != motorista.Id)
            {
                return BadRequest();
            }

            var existe = await _context.Motorista.AsNoTracking()
                                                 .AnyAsync(m => m.Id == id);
            if (!existe)
                return NotFound();

            _context.Entry(motorista).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var motorista = await _context.Motorista.FindAsync(id);

            if (motorista == null)
            {
                return NotFound();
            }

            motorista.Status = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}