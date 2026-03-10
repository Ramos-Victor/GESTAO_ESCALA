using Gestao_Escala.Models;

namespace Gestao_Escala.Domain.Interface
{
    public interface IMotoristaService
    {
        Task<IEnumerable<Motorista>> ListarTudoAsync();
        Task<Motorista> CriarEscalaAsync(Motorista motorista);
    }
}