using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Escala.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EscalasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EscalasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Escala>>> Get()
        {
            return await _context.Escala.Include(e => e.Motorista).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Escala>> GetPorId(int id)
        {
            var escala = await _context.Escala.FindAsync(id);

            if (escala == null)
            {
                return NotFound();
            }

            return escala;
        }

        [HttpPost]
        public async Task<ActionResult<Escala>> Post(Escala escala)
        {
            _context.Escala.Add(escala);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPorId), new { id = escala.Id}, escala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Escala escala)
        {
            if (id != escala.Id)
            {
                return BadRequest();
            }

            _context.Entry(escala).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Escala.FindAsync(id);

            if (produto == null)
                return NotFound();

            _context.Escala.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}