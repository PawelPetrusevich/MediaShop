using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    public class NotSubscribedUserException : Exception
    {
        public NotSubscribedUserException()
        {
        }

        public NotSubscribedUserException(string message) : base(message)
        {
        }

        public NotSubscribedUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSubscribedUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}