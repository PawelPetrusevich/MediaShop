using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    /// <summary>
    /// Exception arising when update repository fail
    /// </summary>
    [Serializable]
    public class UpdateProfileException : Exception
    {
        public UpdateProfileException()
        {
        }

        public UpdateProfileException(string message) : base(message)
        {
        }

        public UpdateProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdateProfileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}