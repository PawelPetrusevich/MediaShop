using System;
using System.Runtime.Serialization;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base(Resources.IncorrectPassword)
        {
        }

        public IncorrectPasswordException(string message) : base(message)
        {
        }

        public IncorrectPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}