namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    /// <summary>
    /// State of the account aggregate.
    /// </summary>
    public class AccountState : AggregateState, IStateEvent<SignupExecutedEvent>
    {
        public SignupId Id { get; set; }

        public bool IsCreated { get; private set; }

        public string Email { get; private set; }

        public string Secret { get; private set; }

        public void When(SignupExecutedEvent e)
        {
            this.Id = (SignupId)e.AggregateId;
            this.Email = e.Email;
            this.IsCreated = true;
            this.Secret = e.Secret;
        }
    }
}