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
            //    modelBuilder.Entity<SerialKey>().ToTable("KeyInformations");

            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.Id);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.Key).HasColumnType("varchar").IsRequired().HasMaxLength(80);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.ProcessorId).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.HashValue).HasColumnType("varbinary").IsRequired().HasMaxLength(512);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.CompanyName).HasColumnType("varchar").IsRequired().HasMaxLength(256);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.ExpiryDateTime).HasColumnType("datetime2").IsRequired().HasMaxLength(7);
            //    modelBuilder.Entity<SerialKey>()
            //        .Property(x => x.EncryptedString).HasColumnType("varchar").IsRequired().HasMaxLength(1024);

            //    //key request
            //    modelBuilder.Entity<KeyRequest>().ToTable("KeyRequests");

            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.Id);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.CompanyName).HasColumnType("varchar").IsRequired().HasMaxLength(256);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.ProcessorId).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.Status).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.ExpiryDateTime).HasColumnType("datetime2").IsRequired().HasMaxLength(7);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.UserName).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            //    modelBuilder.Entity<KeyRequest>()
            //        .Property(x => x.Email).HasColumnType("varchar").IsRequired().HasMaxLength(50);

            //seed
            modelBuilder.Entity<ApplicationUserClaim>().HasData(new AdminPolicySeed().AdminClaims);
            modelBuilder.Entity<ApplicationUser>().HasData(new AdminSeed().Admin);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MeetingLink> MeetingLinks { get; set; }
        //public DbSet<KeyRequest> KeyRequests { get; set; }
    }
}