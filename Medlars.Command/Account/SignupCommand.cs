namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    /// <summary>
    /// Signup command. Input parameters. 1. step.
    /// </summary>
    public class SignUpCommand : ICommand
    {
        public AccountId Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Email { get; set; }
    }
}