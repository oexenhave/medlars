namespace Medlars.Query
{
    using System.Data.Entity;

    using Medlars.Query.Migrations;
    using Medlars.Query.Models;

    public class MedlarsDataContext : DbContext
    {
        public MedlarsDataContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedlarsDataContext, Configuration>());
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
