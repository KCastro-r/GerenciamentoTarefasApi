using System.Security.Cryptography;
using System.Text;
using GerenciamentoTarefasApi.Data;
using GerenciamentoTarefasApi.DTO;
using GerenciamentoTarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoTarefasApi.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto?> CadastrarAsync(CadastrarUsuarioDto dto)
        {
            // CORREÇÃO: normaliza o e-mail e impede e-mail duplicado
            // (o banco não tem índice único para isso).
            var email = dto.Email.Trim().ToLowerInvariant();
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                return null;

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = email,
                // CORREÇÃO: a senha é salva com hash, nunca em texto puro.
                Senha = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(dto.Senha)))
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email };
        }
    }
}