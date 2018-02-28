using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when update repository fail
    /// </summary>
    [Serializable]
    public class UpdateAccountException : Exception
    {
        public UpdateAccountException()
        {
        }

        public UpdateAccountException(string message) : base(message)
        {
        }

        public UpdateAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdateAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}