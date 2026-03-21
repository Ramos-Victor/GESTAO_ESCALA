namespace Gestao_Escala.Models
{
    public class VigenciaMotorista
    {
        public int Id { get; set;}
        public DateOnly Data { get; set; }
        public bool Status { get; set; } = true;

        public int VigenciaId { get; set; }
        public Vigencia? Vigencia { get; set; }

        public int MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }
    }
}