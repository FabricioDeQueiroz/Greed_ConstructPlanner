using Microsoft.EntityFrameworkCore;
using Construct_Planner.Models;

namespace Construct_Planner.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Obra> Obras { get; set; }
    }
}