namespace Medlars.Website
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    using Command;
    using Query;
    using Controllers;

    public class DependencyInjectionStartup
    {
        private readonly IContainer container;

        public DependencyInjectionStartup()
        {
            var builder = new ContainerBuilder();

            MedlarsCommandInjection.Inject(ref builder);
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