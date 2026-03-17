namespace Gestao_Escala.Models
{
    public enum TipoJornada
    {
        Manha = 1,
        Tarde = 2,
        Noite = 3,
        Madrugada = 4
    }
    public class Escala
    {
        public int Id { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFinal { get; set; }
        public TimeOnly InicioIntervalo { get; set; }
        public TimeOnly FimIntervalo { get; set; }
        public TipoJornada TipoJornada { get; set; }
        public bool Status { get; set; } = true;

        public ICollection<VigenciaEscala> Vigencias { get; set; } = new List<VigenciaEscala>();
        
    }
}