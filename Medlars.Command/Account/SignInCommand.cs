using System;

namespace Medlars.Command.Account
{
    using TastyDomainDriven;

    public class SignInCommand : ICommand
    {
        public AccountId Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Ip { get; set; }

        public bool Success { get; set; }
    }
}
