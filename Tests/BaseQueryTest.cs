namespace Medlars.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using Autofac;

    using Medlars.Command;
    using Medlars.Query;

    using TastyDomainDriven;
    using TastyDomainDriven.Bus;
    using TastyDomainDriven.EventStore;
    using TastyDomainDriven.File;
    using TastyDomainDriven.Memory;

    public abstract class BaseQueryTest : IDisposable
    {
        private IContainer container;

        public abstract void RegisterTestTypes(ContainerBuilder builder);

        public void Dispose()
        {
            container.Dispose();
        }

        protected void Given(ILifetimeScope scope, bool resetDatabase, IEnumerable<ICommand> commands, Action<MedlarsDataContext> action)
        {
            using (var testScope = scope.BeginLifetimeScope())
            {
                if (resetDatabase)
                {
                    testScope.Resolve<MedlarsDataContext>().ResetDatabase();
                }

                foreach (var command in commands)
                {
                    testScope.Resolve<IBus>().Dispatch(command);
                }

                action(testScope.Resolve<MedlarsDataContext>());
            }
        }

        protected IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MedlarsServiceFactory>().As<ServiceFactory>();
            builder.RegisterType<MemoryAppendStore>().AsImplementedInterfaces().SingleInstance();
            builder.Register(x => new MedlarsDataContext(ConfigurationManager.ConnectionStrings["events"].ConnectionString)).InstancePerLifetimeScope();
            builder.RegisterType<IEventPublisher>().As<BaseEventPublisher>();
            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();

            this.RegisterTestTypes(builder);

            container = builder.Build();

            return container;
        }
    }
}
