namespace Medlars.Command.Entry
{
    using System;

    using TastyDomainDriven;

    public enum EntrySeverity
    {
        Debug,

        Info,

        Warning,

        Error
    }

    public class EntryState : AggregateState, IStateEvent<StringAddedEvent>
    {
        public EntryId Id { get; private set; }

        public bool IsCreated { get; private set; }

        public DateTime Timestamp { get; private set; }

        public EntrySeverity Severity { get; private set; }

        public string MessageString { get; private set; }

        public double MessageDouble { get; private set; }

        public Guid AccountId { get; private set; }

        public void When(StringAddedEvent e)
        {
            this.IsCreated = true;
            this.Timestamp = e.Timestamp;
            this.Severity = e.Severity;
            this.MessageString = e.Message;
        }
    }
}