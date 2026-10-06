 using GerenciamentoTarefasApi.Models;

namespace GerenciamentoTarefasApi.Repository
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ObterTodasPorUsuarioIdAsync(int usuarioId);
        Task<Tarefa?> ObterPorIdAsync(int id);
        Task<Tarefa> CriarAsync(Tarefa tarefa);
        Task AtualizarAsync(Tarefa tarefa);
        Task DeletarAsync(int id);
    }
}
