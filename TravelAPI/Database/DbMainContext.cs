using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using TravelAPI.Core.Models;

namespace TravelAPI.Database
{
    public class DbMainContext : ApiAuthorizationDbContext<User>
    {
        public DbMainContext(DbContextOptions options, IOptions<OperationalStoreOptions> opts): base (options, opts)
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
            modelBuilder.Entity<User>().HasOne(u => u.Company).WithMany(c => c.CompanyUsers);

            modelBuilder.Entity<Company>().HasOne(c => c.Owner);
            modelBuilder.Entity<Company>().HasMany(c => c.Travels).WithOne(t => t.CompanyOrganizer);

            modelBuilder.Entity<Travel>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
