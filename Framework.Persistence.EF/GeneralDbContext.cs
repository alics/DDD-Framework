using Framework.Core;
using Framework.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    public interface IDbContextsAssemblyResolver
    {
        List<Assembly> GetAssemblies();
    }

    public class DbContextsAssemblyResolver : IDbContextsAssemblyResolver
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>() ;
        public DbContextsAssemblyResolver(List<Type> dbContextTypes)
        {
            foreach (var context in dbContextTypes)
            {
                _assemblies.Add(context.Assembly);
            }
        }

        public List<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }

    public class GeneralDbContext : DbContext
    {
        private readonly IDbContextsAssemblyResolver _dbContextsAssemblyResolver;
        private readonly IConfiguration _configuration;

        public GeneralDbContext(DbContextOptions<GeneralDbContext> options, 
            IDbContextsAssemblyResolver dbContextsAssemblyResolver,
            IConfiguration configuration)
            : base(options)
        {
            _dbContextsAssemblyResolver = dbContextsAssemblyResolver;
            _configuration = configuration;
        }

        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var assembly in _dbContextsAssemblyResolver.GetAssemblies())
            {
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }

            base.OnModelCreating(modelBuilder);
        }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var isEnabledAuditEntity = bool.Parse(_configuration.GetSection("Logger:IsEnabledAuditEntity").Value);
            if (isEnabledAuditEntity == true)
            {
                var auditEntries = OnBeforeSaveChanges(_configuration);
            }
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        public override int SaveChanges()
        {
            var isEnabledAuditEntity = bool.Parse(_configuration.GetSection("Logger:IsEnabledAuditEntity").Value);
            if (isEnabledAuditEntity == true)
            {
                var auditEntries = OnBeforeSaveChanges(_configuration);
            }

            return base.SaveChanges();
        }

        protected virtual List<AuditEntry> OnBeforeSaveChanges(IConfiguration configuration)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is OutboxMessage || entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.EntityName = entry.Metadata.GetTableName(); 
                
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }

                auditEntries.Add(auditEntry);
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries)
            {
                Audits.Add(auditEntry.ToAudit(configuration));
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.ToList();
        }
    }

    public class Audit
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string UserIdentity { get; set; }
        public string ApplicationName { get; set; }
        public string IP { get; set; }
    }

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string EntityName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit(IConfiguration configuration)
        {
            var audit = new Audit();
            audit.EntityName = EntityName;
            audit.DateTime = DateTime.UtcNow;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues); // In .NET Core 3.1+, you can use System.Text.Json instead of Json.NET
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.IP = LoggingUtils.GetClientIp();
            audit.UserIdentity = LoggingUtils.GetUserIdentity();
            audit.ApplicationName = LoggingUtils.GetApplicationName(configuration);
            return audit;
        }
    }
}
