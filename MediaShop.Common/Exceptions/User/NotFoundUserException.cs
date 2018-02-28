using System;
using System.Runtime.Serialization;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base(Resources.UserNotFound)
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