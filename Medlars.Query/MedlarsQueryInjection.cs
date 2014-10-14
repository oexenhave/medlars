namespace Medlars.Query
{
    using System.Configuration;

    using Autofac;

    using Medlars.Query.Consumers.Database;
    using Medlars.Query.Consumers.Notifications;

    public class MedlarsQueryInjection
    {
        public static void Inject(ref ContainerBuilder builder)
        {
            builder.Register(x => new MedlarsDataContext(ConfigurationManager.ConnectionStrings["events"].ConnectionString)).InstancePerLifetimeScope();
            builder.RegisterType<AccountView>().AsImplementedInterfaces();

            builder.RegisterType<AccountSignupNotification>().AsImplementedInterfaces();
        }
    }
}
