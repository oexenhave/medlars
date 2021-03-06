﻿namespace Medlars.Command
{
    using Autofac;

    using Account;

    using TastyDomainDriven;
    using TastyDomainDriven.Bus;
    using TastyDomainDriven.EventStore;
    using TastyDomainDriven.Memory;
    using TastyDomainDriven.MsSql;
    using System.Configuration;

    public class MedlarsCommandInjection
    {
        public static void Inject(ref ContainerBuilder builder)
        {
            builder.RegisterType<SynchronBus>().AsImplementedInterfaces();
            builder.RegisterType<MedlarsServiceFactory>().As<ServiceFactory>();
            builder.RegisterType<BaseEventPublisher>().AsImplementedInterfaces();

            builder.RegisterType<AccountService>().AsImplementedInterfaces();
        }
    }
}
