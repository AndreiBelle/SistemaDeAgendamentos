namespace ApiAgendamento.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Observacoes { get; set; }
        public string Responsavel { get; set; }
        public DateTime DatahorarioInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        
    }
}
