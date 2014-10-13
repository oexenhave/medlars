namespace Medlars.Query
{
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    public class MedlarsDataConfiguration : DbConfiguration
    {
        public MedlarsDataConfiguration()
        {
            // Connection Resiliency / Retry Logic: http://msdn.microsoft.com/en-US/data/dn456835
            // Moving DbConfiguration: http://entityframework.codeplex.com/wikipage?title=Code-based%20Configuration
            this.SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
