using System.Text.Json.Serialization;
using TDLembretes.Models;

namespace TDLembretes.DTO
{
    public class AtualizarTarefaPersonalizadaDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataFinalizacao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTarefa Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PrioridadeTarefa Prioridade { get; set; }

        public bool AlarmeAtivado { get; set; } = false;
    }
}
