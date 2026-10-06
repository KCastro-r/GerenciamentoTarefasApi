
using GerenciamentoTarefasApi.Data;
using Microsoft.EntityFrameworkCore;
using GerenciamentoTarefasApi.Models;
namespace GerenciamentoTarefasApi.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasPorUsuarioIdAsync(int usuarioId)
        {
            return await _context.Tarefas
                .Where(t => t.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Tarefa?> ObterPorIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> CriarAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task AtualizarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var tarefa = await ObterPorIdAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }
    }
}