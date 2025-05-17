using System.Text.Json.Serialization;
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

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PrioridadeTarefa Prioridade { get; set; }

        public bool AlarmeAtivado { get; set; }
    }
}
