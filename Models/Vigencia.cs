namespace Gestao_Escala.Models
{
    public class Vigencia
    {
        public int Id { get; set;}
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public bool Status { get; set; } = true;

        public int EscalaId { get; set; }
        public Escala? Escala { get; set; }

        public ICollection<VigenciaMotorista> Motoristas { get; set;} = new List<VigenciaMotorista>();
    }
}