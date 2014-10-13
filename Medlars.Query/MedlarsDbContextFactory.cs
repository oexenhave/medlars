namespace Medlars.Query
{
    using System.Configuration;
    using System.Data.Entity.Infrastructure;

    public class MedlarsDbContextFactory : IDbContextFactory<MedlarsDataContext>
    {
        public MedlarsDataContext Create()
        {
            return new MedlarsDataContext(ConfigurationManager.ConnectionStrings["events"].ConnectionString);
        }
    }
}
