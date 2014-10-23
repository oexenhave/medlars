using System;

namespace Medlars.Command.Account
{
    public class AccountExistsException : Exception
    {
        public AccountExistsException(string message)
            : base(message)
        {
        }
    }
}
