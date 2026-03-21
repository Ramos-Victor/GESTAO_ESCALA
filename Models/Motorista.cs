namespace Gestao_Escala.Models
{
    public class Motorista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string CategoriaCnh { get; set; } = string.Empty;
        public DateOnly VencimentoCnh { get; set; }
        public bool Status { get; set; } = true;
        public DateOnly DataAdmissao { get; set; }

        public ICollection<VigenciaMotorista> Vigencias { get; set; } = new List<VigenciaMotorista>();
    }
}