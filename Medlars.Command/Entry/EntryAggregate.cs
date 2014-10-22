namespace Medlars.Command.Entry
{
    using System;

    using Medlars.Command.Account;

    using TastyDomainDriven;

    [Serializable]
    public struct EntryId : IIdentity
    {
        private readonly Guid id;

        public EntryId(Guid id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return this.id.ToString();
        }
    }

    public class EntryAggregate : AggregateRoot<EntryState>
    {
        public void AddString(EntryId id, string message, EntrySeverity severity, DateTime timestamp, string service, AccountId accountId)
        {
            this.GuardCreateOnly();
            this.Apply(new StringAddedEvent
                       {
                           AggregateId = id,
                           Message = message,
                           Severity = severity,
                           Timestamp = timestamp,
                           Service = service,
                           AccountId = accountId
                       });
        }

        private void GuardCreateOnly()
        {
            if (this.State.IsCreated)
            {
                throw new Exception("Entry cannot be modified");
            }
        }
    }
}
