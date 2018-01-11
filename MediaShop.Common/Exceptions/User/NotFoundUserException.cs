using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("User not found")
        {
        }

        public NotFoundUserException(string message) : base(message)
        {
        }

        public NotFoundUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}