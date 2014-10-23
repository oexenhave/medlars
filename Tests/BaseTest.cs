using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlars.Tests
{
    using Autofac;
    using Autofac.Core.Lifetime;

    using Medlars.Command;
    using Medlars.Command.Account;
    using Medlars.Command.Entry;

    using TastyDomainDriven;
    using TastyDomainDriven.File;

    public class BaseTest : IDisposable
    {
        private IContainer container;

        public BaseTest()
        {
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
            builder.Register(x => new MedlarsProjectionFactory(new EventStore(x.Resolve<IAppendOnlyStore>()), x.Resolve<ILifetimeScope>())).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<AccountService>().AsImplementedInterfaces();
            builder.RegisterType<EntryService>().AsImplementedInterfaces();
            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();
            container = builder.Build();

            return container;
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
