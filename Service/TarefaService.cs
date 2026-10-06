using GerenciamentoTarefasApi.Data; // Ajuste para o namespace do seu DbContext
using GerenciamentoTarefasApi.DTO;

using GerenciamentoTarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasApi.Service
{
    public class TarefaService : ITarefaService
    {
        private readonly AppDbContext _context;

        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TarefaDto>> ObterTarefasDoUsuarioAsync(int usuarioId)
        {
            return await _context.Tarefas
                .AsNoTracking()
                .Where(t => t.UsuarioId == usuarioId)
                .Select(t => new TarefaDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    DataVencimento = t.DataVencimento,
                    Concluida = t.Concluida,
                    UsuarioId = t.UsuarioId
                })
                .ToListAsync();
        }

        public async Task<TarefaDto> CriarTarefaAsync(CriarTarefaDto dto)
        {
            // Mapeamento: CriarTarefaDto -> Tarefa (Entidade)
            var tarefa = new Tarefa
            {
                
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataVencimento = dto.DataVencimento,
                UsuarioId = dto.UsuarioId,
                Concluida = false // Define valor padrão inicial
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            // Mapeamento: Tarefa (Entidade) -> TarefaDto (Resposta)
            return new TarefaDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataVencimento = tarefa.DataVencimento,
                Concluida = tarefa.Concluida,
                UsuarioId = tarefa.UsuarioId
            };
        }

        public async Task<bool> AtualizarTarefaAsync(AtualizarTarefaDto dto)
        {
            var tarefa = await _context.Tarefas.FindAsync(dto.Id);

            if (tarefa == null)
                return false;

            // Atualização dos campos a partir do DTO
            tarefa.Titulo = dto.Titulo;
            tarefa.Descricao = dto.Descricao;
            tarefa.DataVencimento = dto.DataVencimento;
            tarefa.Concluida = dto.Concluida;
            tarefa.UsuarioId = dto.UsuarioId;

            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConcluirTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            tarefa.Concluida = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}