using System.ComponentModel.DataAnnotations;

namespace GerenciamentoTarefasApi.Models;

public class Tarefa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A descrição pode ter no máximo 500 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataVencimento { get; set; }

    public bool Concluida { get; set; } = false;

    // Relacionamento para saber de qual usuária é a tarefa
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}