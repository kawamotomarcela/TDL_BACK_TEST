using TDLembretes.Models;

namespace TDLembretes.DTO
{
    public class TarefaPersonalizadaDTO
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public PrioridadeTarefa Prioridade { get; set; }
        public bool AlarmeAtivado { get; set; }
    }
}
