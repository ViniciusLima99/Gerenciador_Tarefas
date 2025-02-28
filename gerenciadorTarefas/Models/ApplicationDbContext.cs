using Microsoft.EntityFrameworkCore;

namespace gerenciadorTarefas.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tarefas> Tarefas { get; set; }
    }
}
