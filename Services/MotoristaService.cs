using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Domain.Interfaces;
namespace Gestao_Escala.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly AppDbContext _context;

        public MotoristaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Motorista>> ListarTudoAsync(int page, int pageSize)
        {
            return await _context.Motorista.AsNoTracking().OrderBy(e=> e.Id).Skip((page-1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Motorista?> ObterPorIdAsync(int id)
        {
            return await _context.Motorista.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Motorista> CriarMotoristaAsync(Motorista motorista)
        {
            _context.Motorista.Add(motorista);
            await _context.SaveChangesAsync();
            return motorista;
        }

        public async Task AtualizarMotoristaAsync(Motorista motorista)
        {
            _context.Entry(motorista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MotoristaExiste(motorista.Id))
                    throw new Exception("Motorista não encontrado.");
                else throw;
            }
        }

        private async Task<bool> MotoristaExiste(int id)
        {
            return await _context.Escala.AnyAsync(e => e.Id == id);
        }
    }
}
