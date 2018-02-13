using System;
using System.Runtime.Serialization;

namespace MediaShop.Common.Exceptions.NotificationExceptions
{
    public class EmailTemplatePathException : Exception
    {
        public EmailTemplatePathException()
        {
        }

        public EmailTemplatePathException(string message) : base(message)
        {
        }

        public EmailTemplatePathException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailTemplatePathException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}