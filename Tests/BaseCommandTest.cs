namespace Medlars.Tests
{
    using System;
    using System.Collections.Generic;

    using Autofac;

    using Medlars.Command;

    using TastyDomainDriven;
    using TastyDomainDriven.Bus;
    using TastyDomainDriven.EventStore;
    using TastyDomainDriven.File;
    using TastyDomainDriven.Memory;

    public abstract class BaseCommandTest : IDisposable
    {
        private IContainer container;

        public abstract void RegisterTestTypes(ContainerBuilder builder);

        public void Dispose()
        {
            container.Dispose();
        }

        protected void Given(ILifetimeScope scope, IEnumerable<ICommand> commands, Action<IEventStore> action)
        {
            using (var testScope = scope.BeginLifetimeScope())
            {
                foreach (var command in commands)
                {
                    testScope.Resolve<IBus>().Dispatch(command);
                }

                action(testScope.Resolve<IEventStore>());
            }
        }

        protected void Given(ILifetimeScope scope, IEnumerable<ICommand> commands, Action<IBus> action)
        {
            using (var testScope = scope.BeginLifetimeScope())
            {
                foreach (var command in commands)
                {
                    testScope.Resolve<IBus>().Dispatch(command);
                }

                action(testScope.Resolve<IBus>());
            }
        }

        protected IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MedlarsServiceFactory>().As<ServiceFactory>();
            builder.RegisterType<MemoryAppendStore>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<IEventPublisher>().As<BaseEventPublisher>();
            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();

            this.RegisterTestTypes(builder);

            container = builder.Build();

            return container;
        }
    }
}
