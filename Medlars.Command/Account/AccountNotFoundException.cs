using System;

namespace Medlars.Command.Account
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message)
            : base(message)
        {
        }
    }
}
