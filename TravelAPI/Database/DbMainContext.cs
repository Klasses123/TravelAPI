using Microsoft.EntityFrameworkCore;

namespace TravelAPI.Database
{
    public class DbMainContext : DbContext
    {
        public DbMainContext(DbContextOptions options): base (options)
        {
            Initialize();
        }

        private void Initialize()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;                             // manual changes tracking, increasing working speed 4x times
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;     // Equivalent of .AsNoTracking() for each select query
            Database.AutoTransactionsEnabled = true;                                    // Required for "Unit of work pattern"
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
