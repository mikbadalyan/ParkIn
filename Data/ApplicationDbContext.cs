using Microsoft.EntityFrameworkCore;

namespace ParkIn.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets (tables) here
        public DbSet<YourEntity> YourEntities { get; set; }
    }
}
