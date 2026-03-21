using Gestao_Escala.Models;

namespace Gestao_Escala.interfaces
{
    public interface IVigencia
    {
        Task<PaginacaoResultado<Vigencia>> ListarTudoAsync(int page, int pageSize);
        Task<Vigencia?> ObterPorIdAsync(int id);
        Task<Vigencia> CriarVigenciaAsync (Vigencia Vigencia);
        Task AtualizarVigenciaAsync (Vigencia Vigencia);
        Task<bool> DeletarVigenciaAsync(int id);
    }
}
