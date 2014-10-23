using System;

namespace Medlars.Command.Entry
{
    public class AccoutMissingException : Exception
    {
        public AccoutMissingException(string message)
            : base(message)
        {
        }
    }
}
