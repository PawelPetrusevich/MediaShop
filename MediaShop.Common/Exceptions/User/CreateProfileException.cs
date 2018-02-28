using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    [Serializable]
    public class CreateProfileException : Exception
    {
        public CreateProfileException()
        {
        }

        public CreateProfileException(string message) : base(message)
        {
        }

        public CreateProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateProfileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}