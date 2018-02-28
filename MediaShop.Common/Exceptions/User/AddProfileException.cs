using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when add to repository fail
    /// </summary>
    [Serializable]
    public class AddProfileException : Exception
    {
        public AddProfileException()
        {
        }

        public AddProfileException(string message) : base(message)
        {
        }

        public AddProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddProfileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}