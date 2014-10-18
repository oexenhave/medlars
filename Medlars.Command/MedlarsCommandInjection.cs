namespace Medlars.Command
{
    using System.Configuration;

    using Autofac;

    using Medlars.Command.Account;

    using TastyDomainDriven;
    using TastyDomainDriven.MsSql;

    public class MedlarsCommandInjection
    {
        public static void Inject(ref ContainerBuilder builder)
        {
            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();
            builder.RegisterType<MedlarsServiceFactory>().As<ServiceFactory>();
            builder.Register(x => new MedlarsProjectionFactory(new EventStore(new SqlAppendOnlyStore(ConfigurationManager.ConnectionStrings["events"].ConnectionString)), x.Resolve<ILifetimeScope>())).AsSelf().AsImplementedInterfaces();

            builder.RegisterType<AccountService>().AsImplementedInterfaces();
        }
    }
}
