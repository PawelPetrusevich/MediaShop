using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    public class ConfirmationTokenException : Exception
    {
        public ConfirmationTokenException()
        {
        }

        public ConfirmationTokenException(string message) : base(message)
        {
        }

        public ConfirmationTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfirmationTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}