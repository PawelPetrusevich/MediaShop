using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when add to repository fail
    /// </summary>
    [Serializable]
    public class AddAccountException : Exception
    {
        public AddAccountException() 
        {
        }

        public AddAccountException(string message) : base(message)
        {
        }

        public AddAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}