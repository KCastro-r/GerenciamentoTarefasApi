using GerenciamentoTarefasApi.Models;
using GerenciamentoTarefasApi.DTO;

namespace GerenciamentoTarefasApi.Service
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaDto>> ObterTarefasDoUsuarioAsync(int usuarioId);
        Task<TarefaDto> CriarTarefaAsync(CriarTarefaDto dto);
        Task<bool> AtualizarTarefaAsync(AtualizarTarefaDto dto);
        Task<bool> ConcluirTarefaAsync(int id);
        Task<bool> DeletarTarefaAsync(int id);
    }
}
