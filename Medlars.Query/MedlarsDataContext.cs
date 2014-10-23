namespace Medlars.Query
{
    using System.Data.Entity;

    using log4net;

    using Medlars.Query.Migrations;
    using Medlars.Query.Models;

    public class MedlarsDataContext : DbContext
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MedlarsDataContext));

        public MedlarsDataContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedlarsDataContext, Configuration>());
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public void ResetDatabase()
        {
            this.Database.ExecuteSqlCommand("TRUNCATE TABLE Accounts");
            this.Database.ExecuteSqlCommand("TRUNCATE TABLE Entries");

            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Resetting database!");
            }
        }

        public void TruncateEventStorage()
        {
            this.Database.ExecuteSqlCommand("TRUNCATE TABLE Events");

            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Resetting events!");
            }
        }
    }
}
