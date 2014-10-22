namespace Medlars.Command.Entry
{
    using Medlars.Command.Account;

    using TastyDomainDriven;

    public class EntryState : AggregateState, IStateEvent<StringAddedEvent>
    {
        public EntryId Id { get; private set; }

        public bool IsCreated { get; private set; }

        public AccountId AccountId { get; private set; }

        public void When(StringAddedEvent e)
        {
            this.Id = (EntryId)e.AggregateId;
            this.IsCreated = true;
            this.AccountId = e.AccountId;
        }
    }
}