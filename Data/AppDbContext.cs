using Microsoft.EntityFrameworkCore;
using GerenciamentoTarefasApi.Models;

namespace GerenciamentoTarefasApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
}