using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.CartExseptions
{
    [Serializable]
    public class ConfirmedUserException : Exception
    {
        public ConfirmedUserException() : base("User is already verified")
        {
        }

        public ConfirmedUserException(string message)
            : base(message)
        {
        }

        public ConfirmedUserException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ConfirmedUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}