using System;

namespace Medlars.Command.Entry
{
    public class HashInvalidException : Exception
    {
        public HashInvalidException(string message)
            : base(message)
        {
        }
    }
}
