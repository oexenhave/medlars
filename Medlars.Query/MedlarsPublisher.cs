using Medlars.Query.Consumers.Database;
using Medlars.Query.Consumers.Notifications;
using TastyDomainDriven;
using TastyDomainDriven.Projections;

namespace Medlars.Query
{
    internal class MedlarsPublisher : BaseEventPublisher
    {
        public MedlarsDataContext Context;
        private readonly EventRegister _readRegister;
        private readonly EventRegister _sagaRegister;

        public MedlarsPublisher(MedlarsDataContext context)
        {
            this.Context = context;

            _readRegister = new EventRegister(typeof(IConsumes<>));
            _readRegister.Subscribe(new AccountView(context));
            _readRegister.Subscribe(new AccountSignupNotification());

            _sagaRegister = new EventRegister(typeof(IConsumes<>));
            //sagaRegister.Subscribe
        }

        public override void ConsumeByReadSide(IEvent[] appendedEvents)
        {
            foreach (var item in appendedEvents)
            {
                _readRegister.Consume(item);
            }
        }

        public override void ConsumeBySaga(IEvent[] appendedEvents)
        {
            foreach (var item in appendedEvents)
            {
                _sagaRegister.Consume(item);
            }
        }
    }
}