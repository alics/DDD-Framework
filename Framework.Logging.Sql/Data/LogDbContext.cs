using Microsoft.EntityFrameworkCore;

namespace Framework.Logging.Sql.Data
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<SecurityLog> SecurityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLog");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<SecurityLog>(entity =>
            {
                entity.ToTable("SecurityLog");
                entity.HasKey(p => p.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
