using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    public class AuthorizedDataException : Exception
    {
        public AuthorizedDataException()
        {
        }

        public AuthorizedDataException(string message) : base(message)
        {
        }

        public AuthorizedDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AuthorizedDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}