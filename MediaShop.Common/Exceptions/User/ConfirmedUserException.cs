using System;
using System.Runtime.Serialization;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Exceptions.User
{
    [Serializable]
    public class ConfirmedUserException : Exception
    {
        public ConfirmedUserException() : base(Resources.ConfirmedUser)
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