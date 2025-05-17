using TDLembretes.Repositories.Data;
using TDLembretes.Models;
using Microsoft.EntityFrameworkCore;

namespace TDLembretes.Repositories
{
    public class TarefaPersonalizadaRepository
    {
        private readonly tdlDbContext _context;

        public TarefaPersonalizadaRepository(tdlDbContext context)
        {
            _context = context;
        }

        public async Task AddTarefaPersonalizada(TarefaPersonalizada tarefa)
        {
            await _context.TarefasPersonalizada.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task<TarefaPersonalizada?> GetTarefaPersonalizada(string id)
        {
            return await _context.TarefasPersonalizada
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TarefaPersonalizada>> GetTodas()
        {
            return await _context.TarefasPersonalizada.ToListAsync();
        }

        public async Task UpdateTarefaPersonalizada(TarefaPersonalizada tarefa)
        {
            _context.TarefasPersonalizada.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarefaPersonalizada(TarefaPersonalizada tarefa)
        {
            _context.TarefasPersonalizada.Remove(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task ToggleStatusAsync(string id)
        {
            var tarefa = await GetTarefaPersonalizada(id);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada.");

            tarefa.Status = tarefa.Status switch
            {
                StatusTarefa.Pendente => StatusTarefa.EmAndamento,
                StatusTarefa.EmAndamento => StatusTarefa.Concluida,
                StatusTarefa.Concluida => StatusTarefa.Pendente,
                StatusTarefa.Expirada => StatusTarefa.Pendente,
                _ => tarefa.Status
            };

            await UpdateTarefaPersonalizada(tarefa);
        }
    }
}

