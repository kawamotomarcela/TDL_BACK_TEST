using TDLembretes.DTO;
using TDLembretes.Models;
using TDLembretes.Repositories;

namespace TDLembretes.Services
{
    public class TarefaPersonalizadaService
    {
        private readonly TarefaPersonalizadaRepository _tarefaPersonalizadaRepository;

        public TarefaPersonalizadaService(TarefaPersonalizadaRepository tarefaPersonalizadaRepository)
        {
            _tarefaPersonalizadaRepository = tarefaPersonalizadaRepository;
        }

        public async Task<TarefaPersonalizada> CriarTarefaPersonalizada(TarefaPersonalizadaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo) ||
                string.IsNullOrWhiteSpace(dto.Descricao) ||
                dto.DataFinalizacao == default)
            {
                throw new ArgumentException("Todos os campos devem ser preenchidos corretamente!");
            }

            var novaTarefaPersonalizada = new TarefaPersonalizada(
                Guid.NewGuid().ToString(),
                dto.Titulo,
                dto.Descricao,
                DateTime.UtcNow,
                dto.DataFinalizacao,
                StatusTarefa.Pendente,
                dto.Prioridade,
                dto.AlarmeAtivado
            );

            await _tarefaPersonalizadaRepository.AddTarefaPersonalizada(novaTarefaPersonalizada);

            return novaTarefaPersonalizada; 
        }

        public async Task UpdateTarefaPersonalizada(string id, AtualizarTarefaPersonalizadaDTO dto)
        {
            var tarefa = await _tarefaPersonalizadaRepository.GetTarefaPersonalizada(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            tarefa.Titulo = dto.Titulo;
            tarefa.Descricao = dto.Descricao;
            tarefa.Prioridade = dto.Prioridade;
            tarefa.DataFinalizacao = dto.DataFinalizacao;
            tarefa.Status = dto.Status;

            await _tarefaPersonalizadaRepository.UpdateTarefaPersonalizada(tarefa);
        }

        public async Task DeleteTarefaPersonalizada(string id)
        {
            var tarefa = await _tarefaPersonalizadaRepository.GetTarefaPersonalizada(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            await _tarefaPersonalizadaRepository.DeleteTarefaPersonalizada(tarefa);
        }

        private async Task<TarefaPersonalizada?> GetTarefaPersonalizada(string id)
        {
            return await _tarefaPersonalizadaRepository.GetTarefaPersonalizada(id);
        }

        public async Task<TarefaPersonalizada> GetTarefaPersonalizadaOrThrowException(string id)
        {
            var tarefa = await GetTarefaPersonalizada(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            return tarefa;
        }
    }
}
