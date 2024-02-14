using Microsoft.EntityFrameworkCore;

namespace TestProject.Persistence
{
    public class TaskContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<ExperimentResult> ExperimentResults { get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
                Database.EnsureCreated();
        }
    }
}
