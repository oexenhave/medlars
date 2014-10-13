namespace Medlars.Command
{
    using Autofac;

    using TastyDomainDriven;

    public class MedlarsServiceFactory : ServiceFactory
    {
        private readonly ILifetimeScope scope;

        public MedlarsServiceFactory(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public override IAcceptCommand<T> GetService<T>()
        {
            return this.scope.Resolve<IAcceptCommand<T>>();
        }
    }
}
