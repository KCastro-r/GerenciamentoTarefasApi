using GerenciamentoTarefasApi.DTO;
using GerenciamentoTarefasApi.Models;
using GerenciamentoTarefasApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        [HttpGet("usuario/{usuarioId:int}")]
        public async Task<IActionResult> ObterPorUsuario(int usuarioId)
        {
            var tarefasServico = await _service.ObterTarefasDoUsuarioAsync(usuarioId);

            // Mapeamento das tarefas para o TarefaDto
            var tarefasDto = tarefasServico.Select(t => new TarefaDto
            {
            
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                DataVencimento = t.DataVencimento,
                UsuarioId = t.UsuarioId
            });

            return Ok(ApiResponse<IEnumerable<TarefaDto>>.Ok(tarefasDto));
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTarefaDto tarefaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.Falha("Dados inválidos fornecidos."));

            var novaTarefa = await _service.CriarTarefaAsync(tarefaDto);

            return CreatedAtAction(
                nameof(ObterPorUsuario),
                new { usuarioId = novaTarefa.UsuarioId },
                ApiResponse<TarefaDto>.Ok(novaTarefa, "Tarefa criada com sucesso.")
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarTarefaDto tarefaDto)
        {
            if (id != tarefaDto.Id)
                return BadRequest(ApiResponse<object>.Falha("ID da URL difere do ID da requisição."));

            var atualizado = await _service.AtualizarTarefaAsync(tarefaDto);
            if (!atualizado)
                return NotFound(ApiResponse<object>.Falha("Tarefa não encontrada."));

            return Ok(ApiResponse<string>.Ok("Tarefa atualizada com sucesso."));
        }

        [HttpPatch("{id:int}/concluir")]
        public async Task<IActionResult> Concluir(int id)
        {
            var concluido = await _service.ConcluirTarefaAsync(id);
            if (!concluido)
                return NotFound(ApiResponse<object>.Falha("Tarefa não encontrada."));

            return Ok(ApiResponse<string>.Ok("Tarefa marcada como concluída."));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _service.DeletarTarefaAsync(id);
            if (!deletado)
                return NotFound(ApiResponse<object>.Falha("Tarefa não encontrada."));

            return Ok(ApiResponse<string>.Ok("Tarefa removida com sucesso."));
        }
    }
}