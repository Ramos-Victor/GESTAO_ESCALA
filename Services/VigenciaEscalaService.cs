using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Domain.Interfaces;
using Gestao_Escala.interfaces;

namespace Gestao_Escala.Services
{
    public class VigenciaEscalaService : IVigenciaEscala
    {
        private readonly AppDbContext _context;

        public VigenciaEscalaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultado<VigenciaEscala>> ListarTudoAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);

            var query = _context.VigenciaEscala.AsNoTracking().Where(e=> e.Status == true).OrderBy(e=> e.Id);

            var totalRegistros = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros/ (double)pageSize);

            page = Math.Min(page, Math.Max(1, totalPaginas));

            var dados = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginacaoResultado<VigenciaEscala>
            {
                Dados = dados,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<VigenciaEscala?> ObterPorIdAsync(int id)
        {
            return await _context.VigenciaEscala.FirstOrDefaultAsync(e=> e.Id== id);
        }

        public async Task<VigenciaEscala> CriarVigenciaEscalaAsync(VigenciaEscala vigenciaEscala)
        {
            _context.VigenciaEscala.Add(vigenciaEscala);
            await _context.SaveChangesAsync();
            return vigenciaEscala;
        }

        public async Task AtualizarVigenciaEscalaAsync(VigenciaEscala vigenciaEscala)
        {
            _context.Entry(vigenciaEscala).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletarVigenciaEscalaAsync(int id)
        {
            var vigenciaEscala = await _context.VigenciaEscala.FindAsync(id);
            if(vigenciaEscala == null) return false;

            vigenciaEscala.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}