﻿namespace Medlars.Command.Account
{
    using System;

    using Medlars.Core;

    using TastyDomainDriven;

    /// <summary>
    /// Persists to state to database based on your domain logic
    /// </summary>
    public class AccountAggregate : AggregateRoot<AccountState>
    {
        /// <summary>
        /// Apply business logic. Step 3. 
        /// </summary>
        /// <param name="cmd">The signup command</param>
        public void Signup(SignupCommand cmd)
        {
            this.GuardCreated();

            var secret = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
            var passwordSalt = Encryption.GeneratePasswordSalt();
            var temporaryPassword = Encryption.GenerateRandomPassword();
            var passwordHash = Encryption.GeneratePasswordHash(passwordSalt, temporaryPassword);
            const string AllowedIps = "127.0.0.1 ::1";

            this.Apply(new SignupExecutedEvent
                       {
                           AggregateId = cmd.Id,
                           Email = cmd.Email,
                           Secret = secret,
                           PasswordSalt = passwordSalt,
                           PasswordHash = passwordHash,
                           TemporaryPassword = temporaryPassword,
                           AllowedIps = AllowedIps
                       });
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