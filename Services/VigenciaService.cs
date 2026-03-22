using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.interfaces;

namespace Gestao_Escala.Services
{
    public class VigenciaService : IVigenciaService
    {
        private readonly AppDbContext _context;

        public VigenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultado<Vigencia>> ListarTudoAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);

            var query = _context.Vigencia.AsNoTracking().Where(e=> e.Status == true).OrderBy(e=> e.Id);

            var totalRegistros = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros/ (double)pageSize);

            page = Math.Min(page, Math.Max(1, totalPaginas));

            var dados = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginacaoResultado<Vigencia>
            {
                Dados = dados,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<Vigencia?> ObterPorIdAsync(int id)
        {
            return await _context.Vigencia.FirstOrDefaultAsync(e=> e.Id== id);
        }

        public async Task<Vigencia> CriarVigenciaAsync(Vigencia Vigencia)
        {
            _context.Vigencia.Add(Vigencia);
            await _context.SaveChangesAsync();
            return Vigencia;
        }

        public async Task AtualizarVigenciaAsync(Vigencia Vigencia)
        {
            _context.Entry(Vigencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletarVigenciaAsync(int id)
        {
            var Vigencia = await _context.Vigencia.FindAsync(id);
            if(Vigencia == null) return false;

            Vigencia.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}