using System;
using System.Text.Json.Serialization;

namespace TDLembretes.Models
{
    public class TarefaPersonalizada
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

        public TarefaPersonalizada() { }

        public TarefaPersonalizada(
            string id,
            string titulo,
            string descricao,
            DateTime dataCriacao,
            DateTime dataFinalizacao,
            StatusTarefa status,
            PrioridadeTarefa prioridade,
            bool alarmeAtivado
        )
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            DataFinalizacao = dataFinalizacao;
            Status = status;
            Prioridade = prioridade;
            AlarmeAtivado = alarmeAtivado;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusTarefa
    {
        Pendente,
        EmAndamento,
        Concluida,
        Expirada
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PrioridadeTarefa
    {
        Baixa,
        Media,
        Alta
    }
}
