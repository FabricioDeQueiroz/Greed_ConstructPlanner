using Construct_Planner_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Construct_Planner_API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Obra)
                .WithMany()
                .HasForeignKey(a => a.ObraId);
        }
    }
}