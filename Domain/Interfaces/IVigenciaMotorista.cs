using Gestao_Escala.Models;

namespace Gestao_Escala.interfaces
{
    public interface IVigenciaMotorista
    {
        Task<PaginacaoResultado<VigenciaMotorista>> ListarTudoAsync(int page, int pageSize);
        Task<VigenciaMotorista?> ObterPorIdAsync (int id);
        Task<VigenciaMotorista> CriarVigenciaMotoristaAsync(VigenciaMotorista vigenciaMotorista);
        Task AtualizarVigenciaMotoristaAsync(VigenciaMotorista vigenciaMotorista);
        Task<bool> DeletarVigenciaMotoristaAsync(int id);        
    }
}