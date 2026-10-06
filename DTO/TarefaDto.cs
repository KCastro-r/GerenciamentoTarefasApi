using System.ComponentModel.DataAnnotations;

namespace GerenciamentoTarefasApi.DTO
{
    public class TarefaDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public DateTime? DataVencimento { get; set; }

        public int UsuarioId { get; set; }

        public bool Concluida { get; set; }
    }

    public class CriarTarefaDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }

    // DTO para atualização de tarefa
    public class AtualizarTarefaDto
    {
        [Required(ErrorMessage = "O ID da tarefa é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime DataVencimento { get; set; }

        public bool Concluida { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }
    }
}
