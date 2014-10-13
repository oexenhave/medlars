namespace Medlars.Website
{
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    using Medlars.Command;
    using Medlars.Command.Account;
    using Medlars.Query;
    using Medlars.Website.Controllers;

    using TastyDomainDriven;
    using TastyDomainDriven.MsSql;

    public class DependencyInjectionStartup
    {
        private readonly IContainer container;

        public DependencyInjectionStartup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();
            builder.RegisterType<MedlarsServiceFactory>().As<ServiceFactory>();
            builder.Register(x => new MedlarsProjectionFactory(new EventStore(new SqlAppendOnlyStore(ConfigurationManager.ConnectionStrings["events"].ConnectionString)), x.Resolve<ILifetimeScope>())).AsSelf().AsImplementedInterfaces();

            builder.RegisterType<AccountService>().AsImplementedInterfaces();

            MedlarsQueryInjection.Inject(ref builder);

            // Web project
            builder.RegisterApiControllers(typeof(AccountController).Assembly);
            builder.RegisterControllers(typeof(HomeController).Assembly);

            this.container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(this.container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(this.container);
        }

        public IContainer GetContainer()
        {
            return this.container;
        }
    }
}