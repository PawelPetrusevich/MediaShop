using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    using System.Collections.Generic;

    /// <summary>
    /// Exception arising when user registres with existing login
    /// </summary>
    [Serializable]
    public class ExistingLoginException : Exception
    {
        public ExistingLoginException()
        {
        }

        public ExistingLoginException(string message) : base(message)
        {
        }

        public ExistingLoginException(IEnumerable<string> errors) : base(string.Join(", ", errors))
        {
        }

        public ExistingLoginException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExistingLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}