namespace Medlars.Command
{
    using System.Collections.Generic;

    using Autofac;

    using TastyDomainDriven;

    public class MedlarsProjectionFactory : ProjectionFactory
    {
        private readonly ILifetimeScope scope;

        public MedlarsProjectionFactory(IEventStore eventStore, ILifetimeScope scope)
            : base(eventStore)
        {
            this.scope = scope;
        }

        public override void ConsumeByReadSide<T>(T e)
        {
            foreach (var read in this.scope.Resolve<IEnumerable<IConsumes<T>>>())
            {
                read.Consume(e);
            }
        }

        public override void ConsumeBySaga<T>(T e)
        {
            foreach (var read in this.scope.Resolve<IEnumerable<ISagaConsumes<T>>>())
            {
                read.Consume(e);
            }
        }
    }
}
