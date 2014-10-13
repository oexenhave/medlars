namespace Medlars.Command.Account
{
    using Medlars.Command.Extensions;

    using TastyDomainDriven;

    /// <summary>
    /// Fetches from storage, saves and forwards to bus.
    /// </summary>
    public class AccountService : AggregateService<AccountAggregate>, IAcceptCommand<SignupCommand>
    {
        public AccountService(IEventStore eventStorage)
            : base(eventStorage)
        {
        }

        /// <summary>
        /// Validates the incoming command. 2. step. Allowed to say no.
        /// </summary>
        /// <param name="cmd">The signup command</param>
        public void When(SignupCommand cmd)
        {
            cmd.ValidateTimestamp();
            cmd.ValidateId(c => c.Id);
            cmd.ValidateString(c => c.Email);

            this.Update(cmd.Id, aggregate => aggregate.Signup(cmd));
        }
    }
}