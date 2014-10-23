namespace Medlars.Command.Extensions
{
    using System;

    using Medlars.Command.Account;
    using Medlars.Core;

    using TastyDomainDriven;

    public static class CommandExtensions
    {
        public static void ValidateTimestamp(this ICommand cmd)
        {
            if (cmd.Timestamp <= DateTime.MinValue)
            {
                throw new ArgumentException("Timestamp invalid");
            }
        }

        public static void ValidateId<TClass, TValue>(this TClass cmd, Func<TClass, TValue> action) where TClass : ICommand
        {
            if (action(cmd).Equals(default(TValue)))
            {
                throw new ArgumentException("Id missing");
            }
        }

        public static void ValidateString<TClass, TValue>(this TClass cmd, Func<TClass, TValue> action) where TClass : ICommand
        {
            if (string.IsNullOrWhiteSpace(action(cmd).ToString()))
            {
                throw new ArgumentException("Value invalid");
            }
        }

        public static void ValidateEmail<TClass, TValue>(this TClass cmd, Func<TClass, TValue> action) where TClass : ICommand
        {
            if (action(cmd).ToString().IsNullOrInvalidEmail())
            {
                throw new EmailInvalidException("Email \"" + action(cmd) + "\" is invalid");
            }
        }
    }
}
