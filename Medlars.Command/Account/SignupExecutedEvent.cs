namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    /// <summary>
    /// Action outcome
    /// </summary>
    [Serializable]
    public class SignupExecutedEvent : IEvent
    {
        public IIdentity AggregateId { get; set; }

        public Guid EventId { get; set; }

        public DateTime Timestamp { get; set; }

        public long Version { get; set; }

        public string Email { get; set; }

        public string Secret { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string TemporaryPassword { get; set; }

        public string AllowedIps { get; set; }
    }
}
