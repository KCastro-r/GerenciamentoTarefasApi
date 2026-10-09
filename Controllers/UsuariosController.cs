using GerenciamentoTarefasApi.DTO;
using GerenciamentoTarefasApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoTarefasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        // endpoint de cadastro
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CadastrarUsuarioDto dto)
        {
            var usuario = await _service.CadastrarAsync(dto);

            if (usuario == null)
                return Conflict(ApiResponse<object>.Falha("Já existe uma usuária cadastrada com este e-mail."));

            return StatusCode(201, ApiResponse<UsuarioDto>.Ok(usuario, "Usuária cadastrada com sucesso."));
        }
    }
}