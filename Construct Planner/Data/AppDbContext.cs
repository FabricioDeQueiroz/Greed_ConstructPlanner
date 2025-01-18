using Microsoft.EntityFrameworkCore;
using Task = Construct_Planner.Models.Task;

namespace Construct_Planner.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Task> Tasks { get; set; }
    
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Task>();
        //
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}