using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Exceptions.NotificationExceptions
{
    public class CountOfTryToEmailSendException : Exception
    {
        public CountOfTryToEmailSendException()
        {
        }

        public CountOfTryToEmailSendException(string message) : base(message)
        {
        }

        public CountOfTryToEmailSendException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountOfTryToEmailSendException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
