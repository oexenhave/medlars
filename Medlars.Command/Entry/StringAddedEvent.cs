using System;

namespace Medlars.Command.Entry
{
    using Medlars.Command.Account;

    using TastyDomainDriven;

    [Serializable]
    public class StringAddedEvent : IEvent
    {
        public IIdentity AggregateId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Timestamp { get; set; }

        public long Version { get; set; }

        public AccountId AccountId { get; set; }

        public string Service { get; set; }

        public string Message { get; set; }

        public EntrySeverity Severity { get; set; }
    }
}
