using GerenciamentoTarefasApi.DTO;

namespace GerenciamentoTarefasApi.Service
{
    public interface IUsuarioService
    {
        // Devolve null quando o e-mail já está cadastrado.
        Task<UsuarioDto?> CadastrarAsync(CadastrarUsuarioDto dto);
    }
}