using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Appdbcontext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
    }
}