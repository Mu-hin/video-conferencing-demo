using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoConferencingDemo.Infrastructure.Entities;
using VideoConferencingDemo.Infrastructure.Entities.Identity;
using VideoConferencingDemo.Infrastructure.Seeds;

namespace VideoConferencingDemo.Infrastructure.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ApplicationDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    b => b.MigrationsAssembly(_migrationAssemblyName)
                );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed
            modelBuilder.Entity<ApplicationUserClaim>().HasData(new AdminPolicySeed().AdminClaims);
            modelBuilder.Entity<ApplicationUser>().HasData(new AdminSeed().Admin);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MeetingLink> MeetingLinks { get; set; }
    }
}