using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    using System.Collections.Generic;

    /// <summary>
    /// Exception arising when can not send Email
    /// </summary>
    [Serializable]

    public class CanNotSendEmailException : Exception
    {
        public CanNotSendEmailException()
        {
        }

        public CanNotSendEmailException(string message) : base(message)
        {
        }

        public CanNotSendEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CanNotSendEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}