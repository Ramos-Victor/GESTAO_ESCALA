using Gestao_Escala.Models;

namespace Gestao_Escala.interfaces
{
    public interface IVigenciaEscala
    {
        Task<PaginacaoResultado<VigenciaEscala>> ListarTudoAsync(int page, int pageSize);
        Task<VigenciaEscala?> ObterPorIdAsync(int id);
        Task<VigenciaEscala> CriarVigenciaEscalaAsync (VigenciaEscala vigenciaEscala);
        Task AtualizarVigenciaEscalaAsync (VigenciaEscala vigenciaEscala);
        Task<bool> DeletarVigenciaEscalaAsync(int id);
    }
}
