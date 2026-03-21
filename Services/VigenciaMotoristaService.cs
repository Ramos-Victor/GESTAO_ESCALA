using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Domain.Interfaces;
using Gestao_Escala.interfaces;

namespace Gestao_Escala.Services
{
    public class VigenciaMotoristaService : IVigenciaMotorista
    {
        private readonly AppDbContext _context;

        public VigenciaMotoristaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultado<VigenciaMotorista>> ListarTudoAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);

            var query = _context.VigenciaMotorista.AsNoTracking().Where(e=> e.Status == true).OrderBy(e=> e.Id);

            var totalRegistros = await query.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalRegistros/ (double)pageSize);

            page = Math.Min(page, Math.Max(1, totalPaginas));

            var dados = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginacaoResultado<VigenciaMotorista>
            {
                Dados = dados,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<VigenciaMotorista?> ObterPorIdAsync(int id)
        {
            return await _context.VigenciaMotorista.FirstOrDefaultAsync(e=> e.Id== id);
        }

        public async Task<VigenciaMotorista> CriarVigenciaMotoristaAsync(VigenciaMotorista VigenciaMotorista)
        {
            _context.VigenciaMotorista.Add(VigenciaMotorista);
            await _context.SaveChangesAsync();
            return VigenciaMotorista;
        }

        public async Task AtualizarVigenciaMotoristaAsync(VigenciaMotorista VigenciaMotorista)
        {
            _context.Entry(VigenciaMotorista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletarVigenciaMotoristaAsync(int id)
        {
            var VigenciaMotorista = await _context.VigenciaMotorista.FindAsync(id);
            if(VigenciaMotorista == null) return false;

            VigenciaMotorista.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}