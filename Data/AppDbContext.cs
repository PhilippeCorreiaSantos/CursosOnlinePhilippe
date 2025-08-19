using Microsoft.EntityFrameworkCore;
using CursosOnlinePhilippe.Models;

namespace CursosOnlinePhilippe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}