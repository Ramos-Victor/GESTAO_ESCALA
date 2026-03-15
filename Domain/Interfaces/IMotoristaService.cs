using Gestao_Escala.Models;

namespace Gestao_Escala.Domain.Interfaces
{
    public interface IMotoristaService
    {
        Task<IEnumerable<Motorista>> ListarTudoAsync(int page, int pageSize);
        Task<Motorista?> ObterPorIdAsync(int id);
        Task<Motorista> CriarMotoristaAsync(Motorista motorista);
        Task AtualizarMotoristaAsync(Motorista motorista);
        Task<bool> DeletarMotoristaAsync(int id);
    }
}