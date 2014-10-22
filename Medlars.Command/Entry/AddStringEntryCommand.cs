namespace Medlars.Command.Entry
{
    using System;

    using Medlars.Command.Account;

    using TastyDomainDriven;

    public class AddStringEntryCommand : ICommand
    {
        public EntryId Id { get; set; }

        public DateTime Timestamp { get; set; }

        public EntrySeverity Severity { get; set; }

        public string Service { get; set; }

        public string Message { get; set; }

        public AccountId AccountId { get; set; }

        public string UserHostAddress { get; set; }

        public string Hash { get; set; }
    }
}
