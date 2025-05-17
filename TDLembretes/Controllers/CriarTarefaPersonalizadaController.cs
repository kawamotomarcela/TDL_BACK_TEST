using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDLembretes.DTO;
using TDLembretes.Models;
using TDLembretes.Repositories.Data;
using TDLembretes.Services;

namespace TDLembretes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriarTarefaPersonalizadaController : ControllerBase
    {
        private readonly TarefaPersonalizadaService _tarefaPersonalizadaService;
        private readonly tdlDbContext _context;

        public CriarTarefaPersonalizadaController(
            TarefaPersonalizadaService tarefaPersonalizadaService,
            tdlDbContext context)
        {
            _tarefaPersonalizadaService = tarefaPersonalizadaService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaPersonalizada>>> GetTarefas()
        {
            return await _context.TarefasPersonalizada.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TarefaPersonalizada>> Criar([FromBody] TarefaPersonalizadaDTO dto)
        {
            Console.WriteLine($"🔍 RECEBIDO DTO: {System.Text.Json.JsonSerializer.Serialize(dto)}");

            try
            {
                var tarefa = await _tarefaPersonalizadaService.CriarTarefaPersonalizada(dto);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] AtualizarTarefaPersonalizadaDTO dto)
        {
            try
            {
                await _tarefaPersonalizadaService.UpdateTarefaPersonalizada(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(string id)
        {
            try
            {
                await _tarefaPersonalizadaService.DeleteTarefaPersonalizada(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
        [HttpPut("{id}/toggle")]
        [ProducesResponseType(typeof(TarefaPersonalizada), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AlternarStatus(string id)
        {
            try
            {
                var tarefa = await _tarefaPersonalizadaService.GetTarefaPersonalizadaOrThrowException(id);

                tarefa.Status = tarefa.Status switch
                {
                    StatusTarefa.Pendente => StatusTarefa.EmAndamento,
                    StatusTarefa.EmAndamento => StatusTarefa.Concluida,
                    StatusTarefa.Concluida => StatusTarefa.Pendente,
                    _ => StatusTarefa.Pendente
                };

                await _tarefaPersonalizadaService.UpdateTarefaPersonalizada(tarefa.Id, new AtualizarTarefaPersonalizadaDTO
                {
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    DataFinalizacao = tarefa.DataFinalizacao,
                    Prioridade = tarefa.Prioridade,
                    Status = tarefa.Status
                });

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
