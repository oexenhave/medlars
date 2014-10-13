namespace Medlars.Command.Account
{
    using System;

    using TastyDomainDriven;

    [Serializable]
    public struct SignupId : IIdentity
    {
        private readonly Guid id;

        public SignupId(Guid id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return this.id.ToString();
        }
    }

    /// <summary>
    /// Signup command. Input parameters. 1. step.
    /// </summary>
    public class SignupCommand : ICommand
    {
        public SignupId Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Email { get; set; }
    }
}