using Gestao_Escala.Models;
namespace Gestao_Escala.Domain.Interfaces
{
    public interface IEscalaService
    {
        Task<IEnumerable<Escala>> ListarTudoAsync(int page, int pageSize);
        Task<Escala?> ObterPorIdAsync(int id);
        Task<Escala> CriarEscalaAsync(Escala escala);
        Task AtualizarEscalaAsync(Escala escala);
        Task<bool> DeletarEscalaAsync(int id);
    }
}