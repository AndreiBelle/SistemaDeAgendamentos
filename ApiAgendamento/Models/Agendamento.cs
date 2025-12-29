using Microsoft.EntityFrameworkCore;

namespace ApiAgendamento.Models
{

    [Index(nameof(DatahorarioInicio))]
    [Index(nameof(DataHoraFim))]

    public class Agendamento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Observacoes { get; set; }
        public string Responsavel { get; set; }
        public DateTime DatahorarioInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public Sala salaSelecionada { get; set; }

    }

    public enum Sala
    {
        Sala_12,
        Sala_13
    }

    public class SalaItem
    {
        public Sala Valor {  get; set; }
        public string ValorNome { get; set; }
    }
}
