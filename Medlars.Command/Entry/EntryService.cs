namespace Medlars.Command.Entry
{
    using Medlars.Command.Account;
    using Medlars.Command.Extensions;

    using TastyDomainDriven;

    public class EntryService : AggregateService<EntryAggregate>,
        IAcceptCommand<AddStringEntryCommand>
    {
        public EntryService(IEventStore eventStorage)
            : base(eventStorage)
        {
        }

        public void When(AddStringEntryCommand cmd)
        {
            cmd.ValidateTimestamp();
            cmd.ValidateId(c => c.Id);
            cmd.ValidateId(c => c.AccountId);
            cmd.ValidateString(c => c.Message);
            cmd.ValidateString(c => c.UserHostAddress);
            cmd.ValidateString(c => c.Service);

            this.Create<EntryId, AccountAggregate, AccountId>(cmd.AccountId, aggregate => aggregate.AddString(cmd), cmd.Id);
        }
    }
}
