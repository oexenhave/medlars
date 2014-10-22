namespace Medlars.Command.Entry
{
    using System;

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
        public void AddString(AddStringEntryCommand cmd)
        {
            this.GuardCreateOnly();

            this.Apply(new StringAddedEvent
                       {
                           AggregateId = cmd.Id,
                           Message = cmd.Message,
                           Severity = cmd.Severity,
                           Timestamp = cmd.Timestamp
                       });
        }

        private void GuardCreateOnly()
        {
            if (!this.State.IsCreated)
            {
                throw new Exception("Entry cannot be modified");
            }
        }
    }
}
