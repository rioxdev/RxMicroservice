using Microsoft.EntityFrameworkCore;
using PlateformeService.Models;

namespace PlateformeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts):base(opts) 
        {

        }

        public DbSet<Plateform> Plateforms { get; set; }
    }
}
