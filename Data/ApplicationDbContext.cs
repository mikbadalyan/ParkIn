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
        public DbSet<UserSign> Usersigns { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
    }
}
