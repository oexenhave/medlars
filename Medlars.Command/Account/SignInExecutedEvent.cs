namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    [Serializable]
    public class SignInExecutedEvent : IEvent
    {
        public IIdentity AggregateId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Timestamp { get; set; }

        public long Version { get; set; }
    }
}