using System;
using System.Data.Entity;

namespace Framework.Persistence
{
    public class ReadModelDbContextBase : DbContext
    {
        public ReadModelDbContextBase(string connectionName) : base(connectionName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new RemoveUnderscoreForeignKeyNamingConvention());
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
