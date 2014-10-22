namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    /// <summary>
    /// State of the account aggregate.
    /// </summary>
    public class AccountState : AggregateState,
        IStateEvent<SignUpSucceededEvent>,
        IStateEvent<SignInSucceededEvent>,
        IStateEvent<SignInFailedEvent>
    {
        public AccountId Id { get; set; }

        public bool IsCreated { get; private set; }

        public string Email { get; private set; }

        public string Secret { get; private set; }

        public DateTime LastLogin { get; private set; }

        public string AllowedIps { get; private set; }

        public void When(SignUpSucceededEvent e)
        {
            this.Id = (AccountId)e.AggregateId;
            this.Email = e.Email;
            this.IsCreated = true;
            this.Secret = e.Secret;
            this.LastLogin = e.Timestamp;
            this.AllowedIps = e.AllowedIps;
        }

        public void When(SignInSucceededEvent e)
        {
            this.LastLogin = e.Timestamp;
        }

        public void When(SignInFailedEvent e)
        {
            // throw new NotImplementedException();
        }
    }
}