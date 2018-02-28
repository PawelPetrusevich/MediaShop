using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    /// <summary>
    /// Exception arising when add to repository fail
    /// </summary>
    [Serializable]
    public class AddStatisticException : Exception
    {
        public AddStatisticException()
        {
        }

        public AddStatisticException(string message) : base(message)
        {
        }

        public AddStatisticException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddStatisticException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}