using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.User
{
    /// <summary>
    /// Exception arising when add to repository fail
    /// </summary>
    [Serializable]
    public class DeleteUserException : Exception
    {
        public DeleteUserException()
        {
        }

        public DeleteUserException(string message) : base(message)
        {
        }

        public DeleteUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeleteUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}