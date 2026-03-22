using Gestao_Escala.Models;
namespace Gestao_Escala.Domain.interfaces
{
    public interface IVigenciaMotorista
    {
        Task<IEnumerable<VigenciaMotorista>> ListarPorVigenciaAsync(int vigenciaId);
        Task<VigenciaMotorista?> ObterPorDataAsync(int vigenciaId, DateOnly data);
        Task<VigenciaMotorista?> ObterPorIdAsync(int id);
        Task<VigenciaMotorista> CriarVigenciaMotoristaAsync(int vigenciaId, VigenciaMotorista vigenciaMotorista);
        Task AtualizarVigenciaMotoristaAsync(VigenciaMotorista vigenciaMotorista);
        Task<bool> DeletarVigenciaMotoristaAsync(int id);
    }
}