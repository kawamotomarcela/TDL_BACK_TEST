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

            var novaTarefa = new TarefaPersonalizada(
                Guid.NewGuid().ToString(),
                dto.Titulo,
                dto.Descricao,
                DateTime.UtcNow,
                dto.DataFinalizacao,
                StatusTarefa.Pendente,
                dto.Prioridade,
                dto.AlarmeAtivado
            );

            await _tarefaPersonalizadaRepository.AddTarefaPersonalizada(novaTarefa);
            return novaTarefa;
        }

        public async Task UpdateTarefaPersonalizada(string id, AtualizarTarefaPersonalizadaDTO dto)
        {
            var tarefa = await _tarefaPersonalizadaRepository.GetTarefaPersonalizada(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            tarefa.Titulo = dto.Titulo;
            tarefa.Descricao = dto.Descricao;
            tarefa.DataFinalizacao = dto.DataFinalizacao;
            tarefa.Prioridade = dto.Prioridade;
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

        public async Task<TarefaPersonalizada?> BuscarPorId(string id)
        {
            return await _tarefaPersonalizadaRepository.GetTarefaPersonalizada(id);
        }

        public async Task<TarefaPersonalizada> GetTarefaPersonalizadaOrThrowException(string id)
        {
            var tarefa = await BuscarPorId(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            return tarefa;
        }

        public async Task SalvarAlteracoes(TarefaPersonalizada tarefa)
        {
            await _tarefaPersonalizadaRepository.UpdateTarefaPersonalizada(tarefa);
        }

        public async Task<List<TarefaPersonalizada>> ObterTodasComExpiradasAtualizadas()
        {
            var tarefas = await _tarefaPersonalizadaRepository.GetTodas();
            var agora = DateTime.UtcNow;

            foreach (var tarefa in tarefas)
            {
                if (tarefa.DataFinalizacao < agora &&
                    tarefa.Status != StatusTarefa.Concluida &&
                    tarefa.Status != StatusTarefa.Expirada)
                {
                    tarefa.Status = StatusTarefa.Expirada;
                    await _tarefaPersonalizadaRepository.UpdateTarefaPersonalizada(tarefa);
                }
            }

            return tarefas;
        }
    }
}
