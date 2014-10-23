using System;

namespace Medlars.Command.Account
{
    public class EmailInvalidException : Exception
    {
        public EmailInvalidException(string message)
            : base(message)
        {
        }
    }
}
