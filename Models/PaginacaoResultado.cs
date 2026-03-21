namespace Gestao_Escala.Models
{
    public class PaginacaoResultado<T>
    {
        public IEnumerable<T>? Dados { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public bool TemProxima => PaginaAtual < TotalPaginas;
        public bool TemAnterior => PaginaAtual > 1;
    }
}