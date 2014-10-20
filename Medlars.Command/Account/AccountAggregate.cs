namespace Medlars.Command.Account
{
    using System;

    using Medlars.Core;

    using TastyDomainDriven;

    [Serializable]
    public struct AccountId : IIdentity
    {
        private readonly Guid id;

        public AccountId(Guid id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return this.id.ToString();
        }
    }

    /// <summary>
    /// Persists to state to database based on your domain logic
    /// </summary>
    public class AccountAggregate : AggregateRoot<AccountState>
    {
        /// <summary>
        /// Apply business logic. Step 3. 
        /// </summary>
        /// <param name="cmd">The signup command</param>
        public void Signup(SignUpCommand cmd)
        {
            this.GuardCreated();

            var secret = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
            var passwordSalt = Encryption.GeneratePasswordSalt();
            var temporaryPassword = Encryption.GenerateRandomPassword();
            var passwordHash = Encryption.GeneratePasswordHash(passwordSalt, temporaryPassword);
            const string AllowedIps = "127.0.0.1 ::1";

            this.Apply(new SignUpSucceededEvent
                       {
                           AggregateId = cmd.Id,
                           Email = cmd.Email,
                           Secret = secret,
                           PasswordSalt = passwordSalt,
                           PasswordHash = passwordHash,
                           TemporaryPassword = temporaryPassword,
                           AllowedIps = AllowedIps,
                           Timestamp = cmd.Timestamp
                       });
        }

        public void SignIn(SignInCommand cmd)
        {
            if (cmd.Success)
            {
                this.Apply(new SignInSucceededEvent
                {
                    AggregateId = cmd.Id,
                    Timestamp = cmd.Timestamp
                });
            }
            else
            {
                this.Apply(new SignInFailedEvent
                {
                    AggregateId = cmd.Id,
                    Timestamp = cmd.Timestamp
                });
            }
        }

        private void GuardCreated()
        {
            if (this.State.IsCreated)
            {
                throw new Exception("Account already created");
            }
        }
    }
}