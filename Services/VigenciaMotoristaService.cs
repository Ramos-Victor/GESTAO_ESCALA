using Gestao_Escala.Data;
using Gestao_Escala.Models;
using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Domain.interfaces;

namespace Gestao_Escala.Services
{
    public class VigenciaMotoristaService : IVigenciaMotorista
    {
        private readonly AppDbContext _context;

        public VigenciaMotoristaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VigenciaMotorista>> ListarPorVigenciaAsync(int vigenciaId)
        {
            return await _context.VigenciaMotorista
                .Include(vm => vm.Motorista)
                .AsNoTracking()
                .Where(vm => vm.VigenciaId == vigenciaId && vm.Status == true)
                .OrderBy(vm => vm.Data)
                .ToListAsync();
        }

        public async Task<VigenciaMotorista?> ObterPorDataAsync(int vigenciaId, DateOnly data)
        {
            return await _context.VigenciaMotorista
                .Include(vm => vm.Motorista)
                .AsNoTracking()
                .FirstOrDefaultAsync(vm => vm.VigenciaId == vigenciaId
                                        && vm.Data == data
                                        && vm.Status == true);
        }

        public async Task<VigenciaMotorista?> ObterPorIdAsync(int id)
        {
            return await _context.VigenciaMotorista
                .Include(vm => vm.Motorista)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<VigenciaMotorista> CriarVigenciaMotoristaAsync(int vigenciaId, VigenciaMotorista vigenciaMotorista)
        {
            // Valida se a vigência existe
            var vigencia = await _context.Vigencia.FindAsync(vigenciaId);
            if (vigencia == null)
                throw new Exception("Vigência não encontrada.");

            // Valida se a data está dentro do período da vigência
            if (vigenciaMotorista.Data < vigencia.DataInicio || vigenciaMotorista.Data > vigencia.DataFim)
                throw new Exception("Data fora do período de vigência.");

            // Valida se já existe motorista nesse dia
            var jaExiste = await _context.VigenciaMotorista
                .AnyAsync(vm => vm.VigenciaId == vigenciaId
                             && vm.Data == vigenciaMotorista.Data
                             && vm.Status == true);
            if (jaExiste)
                throw new Exception("Já existe um motorista atribuído para essa data.");

            vigenciaMotorista.VigenciaId = vigenciaId;
            _context.VigenciaMotorista.Add(vigenciaMotorista);
            await _context.SaveChangesAsync();
            return vigenciaMotorista;
        }

        public async Task AtualizarVigenciaMotoristaAsync(VigenciaMotorista vigenciaMotorista)
        {
            _context.Entry(vigenciaMotorista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletarVigenciaMotoristaAsync(int id)
        {
            var vigenciaMotorista = await _context.VigenciaMotorista.FindAsync(id);
            if (vigenciaMotorista == null) return false;
            vigenciaMotorista.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}