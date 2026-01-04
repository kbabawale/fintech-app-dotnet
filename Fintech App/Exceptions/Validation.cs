using System;

namespace Fintech_App.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {

        }
    }

}
