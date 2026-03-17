using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Domain.Interfaces;

namespace Gestao_Escala.Services
{
    public class EscalaService : IEscalaService
    {
        private readonly AppDbContext _context;

        public EscalaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultado<Escala>> ListarTudoAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);

            var query = _context.Escala.Include(e => e.Motorista).AsNoTracking().Where(e => e.Status == true).OrderBy(e=> e.Data);
            
            var totalRegistros = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize);
            
            page = Math.Min(page, Math.Max(1, totalPaginas));

            var dados = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginacaoResultado<Escala>
            {
                Dados = dados,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Escala?> ObterPorIdAsync(int id)
        {
            return await _context.Escala
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Escala> CriarEscalaAsync(Escala escala)
        {
            ValidarHorarios(escala);
            await ValidarSobreposicaoAsync(escala);

            _context.Escala.Add(escala);
            await _context.SaveChangesAsync();
            return escala;
        }

        public async Task AtualizarEscalaAsync(Escala escala)
        {
            ValidarHorarios(escala);
            await ValidarSobreposicaoAsync(escala);

            _context.Entry(escala).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EscalaExiste(escala.Id)) throw new Exception("Escala não encontrada.");
                else throw;
            }
        }

        public async Task<bool> DeletarEscalaAsync(int id)
        {
            var escala = await _context.Escala.FindAsync(id);
            if (escala == null) return false;

            escala.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        private void ValidarHorarios(Escala escala)
        {
            if (escala.HoraInicio >= escala.HoraFinal)
                throw new Exception("A hora de início deve ser anterior à hora final.");

            if (escala.InicioIntervalo < escala.HoraInicio || escala.FimIntervalo > escala.HoraFinal)
                throw new Exception("O intervalo deve estar dentro do período da jornada.");
        }

        private async Task ValidarSobreposicaoAsync(Escala escala)
        {
            if (escala.MotoristaId.HasValue)
            {
                bool sobreposto = await _context.Escala.AnyAsync(e =>
                    e.Id != escala.Id &&
                    e.MotoristaId == escala.MotoristaId &&
                    e.Data == escala.Data &&
                    ((escala.HoraInicio >= e.HoraInicio && escala.HoraInicio < e.HoraFinal) ||
                     (escala.HoraFinal > e.HoraInicio && escala.HoraFinal <= e.HoraFinal)));

                if (sobreposto)
                    throw new Exception("Este motorista já possui uma escala agendada para este horário.");
            }
        }
    }
}