using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions
{
    public class ModelValidateException : Exception
    {
        public ModelValidateException()
        {
        }

        public ModelValidateException(string message) : base(message)
        {
        }

        public ModelValidateException(IEnumerable<string> errors) : base(string.Join(", ", errors))
        {
        }

        public ModelValidateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelValidateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}