namespace Medlars.Query
{
    using System.Configuration;
    using System.Data.Entity.Infrastructure;

    public class MedlarsDbContextFactory : IDbContextFactory<MedlarsDataContext>
    {
        public MedlarsDataContext Create()
        {
            return new MedlarsDataContext("Server=localhost;Database=Medlars;User Id=sa;Password=timelog");
        }
    }
}
