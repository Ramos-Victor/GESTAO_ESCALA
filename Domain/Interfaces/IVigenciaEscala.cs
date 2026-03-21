using Gestao_Escala.Models;

namespace Gestao_Escala.interfaces
{
    public interface IVigenciaEscala
    {
        Task<PaginacaoResultado<VigenciaEscala>> ListarTudoAsync(int page, int pageSize);
        Task<VigenciaEscala?> ObterPorIdAsync(int id);
        Task<VigenciaEscala> CriarVigenciaAsync (VigenciaEscala vigenciaEscala);
        Task AtualizarVigenciaAsync (VigenciaEscala vigenciaEscala);
        Task<bool> DeletarEscalaAsync(int id);
    }
}
